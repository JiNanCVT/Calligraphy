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

namespace CalligraphyEditor.ViewModel
{
    public class VMRubbings 
    {

        ObservableCollection<T_Rubbing> _Ocl_Rubbings;

        public ObservableCollection<T_Rubbing> Ocl_Rubbings
        {            
            get
            {
                return _Ocl_Rubbings;
            }
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
        
        public string AddRubbing(T_Rubbing rubbing, string[] photoPaths)
        {
            string errorMsg = IsValidatedPhoto(photoPaths);
            if (!string.IsNullOrEmpty(errorMsg))
                return errorMsg;

            CalligraphyEditor.App.Entities.AddToT_Rubbing(rubbing);
            CalligraphyEditor.App.Entities.SaveChanges();
            var q = from f in photoPaths orderby f select f;
            photoPaths = q.ToArray<string>();
            List<T_RubbingPhoto> rubbingPhotos = new List<T_RubbingPhoto>(photoPaths.Length);
            int pageNumber = 1;
            foreach (var photoPath in photoPaths)
            {
                T_RubbingPhoto rubbingPhoto = UploadPhoto(photoPath);
                if (rubbingPhoto != null)
                {
                    rubbingPhoto.PageNumber = pageNumber;
                    pageNumber++;
                    rubbingPhotos.Add(rubbingPhoto);
                }
            }            

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
