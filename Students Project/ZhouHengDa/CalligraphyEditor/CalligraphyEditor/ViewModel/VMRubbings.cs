using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using CalligraphyEditor.CalligraphyData;
using CalligraphyEditor.ViewModel;
using System.Windows;
using System.IO;
using System.Windows.Media.Imaging;
using System.Data.Services.Client;
using System.Windows.Data;
using System.Collections;

namespace CalligraphyEditor.ViewModel
{
    public class VMRubbings 
    {
        public VMRubbings()
        { }
        ObservableCollection<T_Rubbing> _Ocl_Rubbings;

        public ObservableCollection<T_Rubbing> Ocl_Rubbings
        {            
            get
            {
                return _Ocl_Rubbings;
            }
        }
        ObservableCollection<T_RubbingPhoto> rubbingPhotos;

        public ObservableCollection<T_RubbingPhoto> RubbingPhotos
        {
            get { return rubbingPhotos; }
            set { rubbingPhotos = value; }
        }

        

        CollectionViewSource _Cvs_Rubbings;

        public CollectionViewSource Cvs_Rubbings
        {
            get { return _Cvs_Rubbings; }
            set { _Cvs_Rubbings = value; }
        }

        public T_Author _author;

        public T_Author Author
        {
            get 
            {
                return _author;
            }
        }

        public VMRubbings(T_Author a)
        {            
            _author = a;

            CreatRubbingCollection();

            _Cvs_Rubbings = new CollectionViewSource();

            _Cvs_Rubbings.Source = _Ocl_Rubbings;
        }

        private void CreatRubbingCollection()
        {
            var q = from r in CalligraphyEditor.App.Entities.T_Rubbing.Expand("T_RubbingPhoto").Expand("T_RubbingPhoto/T_Photo").Expand("T_Author") where r.T_Author.ID.Equals(_author.ID) && (r.IsDeleted == null || r.IsDeleted == false) select r;

            _Ocl_Rubbings = new ObservableCollection<T_Rubbing>(q);
        }

        private T_Rubbing GetCurrentRubbing()
        {
            T_Rubbing r = _Cvs_Rubbings.View.CurrentItem as T_Rubbing;
            return r;
        }
        private string GetExtensionName(string filePath)
        {
            var strs = filePath.Split('.');
            if (strs.Length != 2)
                return string.Empty;
            return strs[1].ToLower();
        }

        private bool IsPhoto(string filePath)
        {
            var extensionName = GetExtensionName(filePath);
            return extensionName == "jpg" || extensionName == "jpeg" || extensionName == "bmp" || extensionName == "png";
        }

        

        private T_RubbingPhoto UploadPhoto(string filePath)
        {
            if (!IsPhoto(filePath))
                return null;

            var fs = new FileStream(filePath, FileMode.Open);
            T_Photo photo = UploadPhoto(fs, GetExtensionName(filePath));
            T_RubbingPhoto rubbingPhoto = new T_RubbingPhoto();
            rubbingPhoto.T_Photo = photo;
            rubbingPhoto.PhotoID = photo.ID;
            //rubbingPhoto.PageNumber = int.Parse(GetFileNameWithoutExtension(filePath));
            photo.T_RubbingPhoto.Add(rubbingPhoto);
            return rubbingPhoto;
        }

        private T_Photo UploadPhoto(FileStream fileStream, string fileExtension)
        {
            EntityDescriptor entity = null;

            T_Photo p = new T_Photo();
            
            p.ContentType = "image/" + fileExtension;

            CalligraphyEditor.App.Entities.AddToT_Photo(p);
            BitmapImage imageFromStream = new BitmapImage();
            imageFromStream.BeginInit();
            imageFromStream.StreamSource = fileStream;
            imageFromStream.CacheOption = BitmapCacheOption.OnLoad;
            imageFromStream.EndInit();

            p.Width = imageFromStream.Width;
            p.Height = imageFromStream.Height;
            p.FileSize = fileStream.Length;

            fileStream.Seek(0, SeekOrigin.Begin);

            CalligraphyEditor.App.Entities.SetSaveStream(p, fileStream, true, p.ContentType, GetSafeFileName(fileStream.Name));



            try
            {
                var response = CalligraphyEditor.App.Entities.SaveChanges().FirstOrDefault() as ChangeOperationResponse;

                if (p.ID == 0)
                {
                    if (response != null)
                    {
                        entity = response.Descriptor as EntityDescriptor;
                    }

                    // Verify that the entity was created correctly.
                    if (entity != null && entity.EditLink != null)
                    {
                        // Cache the current merge option (we reset to the cached 
                        // value in the finally block).
                        MergeOption mergeOption = CalligraphyEditor.App.Entities.MergeOption;

                        try
                        {
                            // Set the merge option so that server changes win.
                            CalligraphyEditor.App.Entities.MergeOption = MergeOption.OverwriteChanges;

                            // Get the updated entity from the service.
                            // Note: we need Count() just to execute the query.
                            CalligraphyEditor.App.Entities.Execute<T_Photo>(entity.EditLink).Count();
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Debug.WriteLine(e.Message);
                        }
                        finally
                        {
                            CalligraphyEditor.App.Entities.MergeOption = mergeOption;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e.Message);
            }
            
            return p;
        }

        public string GetSafeFileName(string fullPath)
        {
            var strs = fullPath.Split('\\');
            return strs[strs.Length - 1];
        }

        public T_Rubbing NewRubbing()
        {
            T_Rubbing r = new T_Rubbing();
            r.T_Author = _author;
            r.AuthorID = _author.ID;
            Author.T_Rubbing.Add(r);
            return r;
        }

        public string IsValidatedPhoto(string[] photoPaths)
        {
            List<int> photoNumbers = new List<int>(photoPaths.Length);
            foreach (var pp in photoPaths)
            {
                if (IsPhoto(pp) == false)
                {
                    return "文件夹内包含不是图片的文件。";
                }
                
            }
            return string.Empty;
        }

        private string GetFileNameWithoutExtension(string path)
        {
            var safePath = GetSafeFileName(path);
            var strs = safePath.Split('.');
            return strs[0].Trim();
        }

        private string[] GetPhotoPaths(string[] Paths)
        {
            List<string> photoPaths1 = new List<string>();
            int i = 0;
            foreach (var p in Paths)
            {
                string extensionName = GetExtensionName(p);
                if (extensionName == "jpg" || extensionName == "jpeg" || extensionName == "bmp" || extensionName == "png")
                {
                    photoPaths1.Add(p);
                }
            }
            string[] photoPaths = new string[photoPaths1.Count] ;
            foreach (var p in photoPaths1)
            {
                photoPaths[i] = p;
                i++;
            }
            return photoPaths;
        }

        private string[] GetTextPaths(string[] Paths)
        {
            int i = 0;
            List<string> textPaths1 = new List<string>();
            foreach (var p in Paths)
            {
                string extensionName = GetExtensionName(p);
                if (extensionName == "txt")
                {
                    textPaths1.Add(p);
                }
            }
            string[] textPaths = new string[textPaths1.Count()];
            foreach (var t in textPaths1)
            {
                textPaths[i] = t;
                i++;
            }
            return textPaths;
        }
        //tiff,gif,pcx,tga,exif,fpx,svg,psd,cdr,pcd,dxf,ufo,eps,ai,raw

        

        public string GetErrorMsg(string[] paths, string[] photoPaths, string[] textPaths)
        {
            string errorMsg = null;
            int i = 0;
            int[] photoNames1 = new int[photoPaths.Length];
            int[] photoNames2 = new int[photoPaths.Length];
            //检查文件夹中是否有除了‘JPG’，‘BMP’，‘JPEG’，‘PNG’格式外的图片
            foreach (var path in paths)
            {
                string extensionName = GetExtensionName(path);
                if (extensionName == "tiff" || extensionName == "gif" || extensionName == "pcx" ||
                    extensionName == "tga" || extensionName == "exif" || extensionName == "fpx" ||
                    extensionName == "cdr" || extensionName == "psd" || extensionName == "svg" ||
                    extensionName == "pcd" || extensionName == "dxf" || extensionName == "ufo" ||
                    extensionName == "raw" || extensionName == "ai" || extensionName == "eps")
                {
               
                    errorMsg = "文件内图片格式不正确,请检查文件夹中是否有除了‘JPG’，‘BMP’，‘JPEG’，‘PNG’格式外的图片";
                    goto finish;
                }
            }
            //return errorMsg;
            
            //检查文件夹中是否有其他格式的文件
            
            if (paths.Length > (photoPaths.Length + textPaths.Length))
            {
                errorMsg = "请移除文件夹内除‘JPG’，‘BMP’，‘JPEG’，‘BMP’，‘PNG’格式的图片和‘txt’格式的文本的其他内容";
                goto finish;
            }

            //检查 文件夹中图片是否以连续的阿拉伯数字命名
            //foreach (var photoPath in photoPaths)
            //{
                
            //    string nameWithoutExtension = GetFileNameWithoutExtension(GetSafeFileName(photoPath));
            //    bool isInt = int.TryParse(nameWithoutExtension, out i);
            //    if (isInt == false)
            //    {
            //        errorMsg = "图片命名不正确，请用连续的阿拉伯数字命名";
            //        goto finish;
            //    }
            //    photoNames1[i] = int.Parse(nameWithoutExtension);
            //    i++;
            //}
            for (i = 0; i < photoPaths.Length; i++)
            {
                int j = 0;
                string nameWithoutExtension = GetFileNameWithoutExtension(GetSafeFileName(photoPaths[i]));
                bool isInt = int.TryParse(nameWithoutExtension, out j);
                if (isInt == false)
                {
                    errorMsg = "图片命名不正确，请用连续的阿拉伯数字命名";
                    goto finish;
                }
                photoNames1[i] = int.Parse(nameWithoutExtension);
      
            }
            
            //检查文件夹中的图片是否有缺漏
            var p = from pn in photoNames1 orderby pn select pn;
            photoNames2 = p.ToArray<int>();
            for (i = 0; i < photoNames2.Length; i++)
            {
                if (photoNames2[i] != (i+1))
                {
                    errorMsg = "文件夹中图片有缺漏或命名错误，请用连续的阿拉伯数字命名";
                    goto finish;
                }
            }

            //检查文件夹中的文本文件命名是否正确
            for(i = 0;i<textPaths.Length;i++)
            {
                MessageBox.Show(textPaths.Length.ToString());
              string nameWithoutExtension = GetFileNameWithoutExtension(GetSafeFileName(textPaths[i]));
              bool isInt = int.TryParse(nameWithoutExtension, out i);
              
              if (isInt == false )
              {
                  errorMsg = "文本文件命名不正确，请用与对应的图片名字相同的数字命名";
                  goto finish;
              }
              int name = int.Parse(nameWithoutExtension);
              if (name > photoPaths.Length || name < 1)
              {
                  errorMsg = "文本文件命名不正确，请用与对应的图片名字相同的数字命名";
                  goto finish;
              }
              }
            finish:
              return errorMsg;
        }

        public string AddRubbing(T_Rubbing rubbing, string[] Paths)
        {

            string[] photoPaths = GetPhotoPaths(Paths);
            string[] textPaths = GetTextPaths(Paths);

            //string errorMsg = IsValidatedPhoto(photoPaths);
            string errorMsg = GetErrorMsg(Paths, photoPaths, textPaths);
            if (!string.IsNullOrEmpty(errorMsg))
                return errorMsg;

            //MainViewModel mvm = new MainViewModel(photoPaths);
            //foreach (var p in photoPaths)
            //{
            //    if(p!=null)
            //    mvm.Images.Add(p);
            //}
            

            CalligraphyEditor.App.Entities.AddToT_Rubbing(rubbing);
            CalligraphyEditor.App.Entities.SaveChanges();
            var q = from f in photoPaths orderby f select f;
            photoPaths = q.ToArray<string>();

            rubbingPhotos = new ObservableCollection<T_RubbingPhoto>();
            int pageNumber = 1;
            int i,j;
            for (i = 0, j = 0; i < Math.Max(photoPaths.Length, textPaths.Length) || j < Math.Max(photoPaths.Length, textPaths.Length); i++, j++)
            {
                if (j < textPaths.Length)
                {
                    string PhotoNameWithoutExtension = GetFileNameWithoutExtension(GetSafeFileName(photoPaths[i]));
                    string TextNameWithoutExtension = GetFileNameWithoutExtension(GetSafeFileName(textPaths[j]));
                    T_RubbingPhoto rubbingPhoto = UploadPhoto(photoPaths[i]);
                    string txtString = GetTxtString(textPaths[j]);
                    if (rubbingPhoto != null)
                    {
                        rubbingPhoto.PageNumber = pageNumber;
                        pageNumber++;
                        if (PhotoNameWithoutExtension.Equals(TextNameWithoutExtension))
                        {
                            rubbingPhoto.Name = txtString;
                        }
                    }
                    MessageBox.Show(rubbingPhoto.PhotoBitmapImage);
                    rubbingPhotos.Add(rubbingPhoto);
                }
            
            }
            Window1 w1 = new Window1();
            w1.ShowDialog();
            ShowPhotoAndText spt = new ShowPhotoAndText();
            //spt.DataContext = mvm;
            spt.ShowDialog();
                //foreach (var photoPath in photoPaths)
                //{
                //    T_RubbingPhoto rubbingPhoto = UploadPhoto(photoPath);
                //    if (rubbingPhoto != null)
                //    {
                //        rubbingPhoto.PageNumber = pageNumber;
                //        pageNumber++;
                //        rubbingPhotos.Add(rubbingPhoto);
                //    }
                //}            

            foreach (var rubbingPhoto in rubbingPhotos)
            {
                rubbingPhoto.RubbingID = rubbing.ID;
                rubbingPhoto.T_Rubbing = rubbing;
                rubbing.T_RubbingPhoto.Add(rubbingPhoto);
                CalligraphyEditor.App.Entities.AddToT_RubbingPhoto(rubbingPhoto);                
                
            }

            CalligraphyEditor.App.Entities.SaveChanges();
            _Ocl_Rubbings.Add(rubbing);

            return string.Empty;
        }
        public string GetTxtString(string txtPath)
        {
            StreamReader objReader = new StreamReader(txtPath);
            string sLine = "";
            List<string> LineList = new List<string>();
            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null && !sLine.Equals(""))
                    LineList.Add(sLine);
            }

            objReader.Close();
            int i;
            string text = "";
            for (i = 0; i < LineList.Count; i++)
            {

                text += LineList[i];
            }
                return text;

        }

        public void DeleteCurrentRubbing()
        {
            T_Rubbing r = GetCurrentRubbing();
            if (r == null)
                return;
            r.IsDeleted = true;
            _Ocl_Rubbings.Remove(r);
            CalligraphyEditor.App.Entities.UpdateObject(r);
            CalligraphyEditor.App.Entities.SaveChanges();
        }

        internal void SaveCurrentItem()
        {
            T_Rubbing r = GetCurrentRubbing();
            if (r == null)
                return;
            CalligraphyEditor.App.Entities.UpdateObject(r);
            CalligraphyEditor.App.Entities.SaveChanges();
        }
    }
}
