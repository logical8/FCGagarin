using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.PL.WebUI.Helpers
{
    public class FilesHelper
    {
        readonly string _deleteUrl;
        readonly string _deleteType;
        readonly string _storageRoot;
        readonly string _urlBase;

        readonly string _tempPath;
        //ex:"~/Files/something/";
        readonly string _serverMapPath;
        public FilesHelper(string deleteUrl, string deleteType, string storageRoot, string urlBase, string tempPath, string serverMapPath)
        {
            _deleteUrl = deleteUrl;
            _deleteType = deleteType;
            _storageRoot = storageRoot;
            _urlBase = urlBase;
            _tempPath = tempPath;
            _serverMapPath = serverMapPath;
        }

        public void DeleteFiles(string pathToDelete)
        {

            var path = HostingEnvironment.MapPath(pathToDelete);

            Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                var di = new DirectoryInfo(path);
                foreach (var fi in di.GetFiles())
                {
                    File.Delete(fi.FullName);
                    Debug.WriteLine(fi.Name);
                }

                di.Delete(true);
            }
        }

        public string DeleteFile(string file, int albumId)
        {
            Debug.WriteLine("DeleteFile");
            //    var req = HttpContext.Current;
            Debug.WriteLine(file);

            var fullPath = Path.Combine(_storageRoot, albumId.ToString(), file);
            Debug.WriteLine(fullPath);
            Debug.WriteLine(File.Exists(fullPath));
            var thumbPath = "/" + file + "80x80.jpg";
            var partThumb1 = Path.Combine(_storageRoot, albumId.ToString(), "thumbs");
            var partThumb2 = Path.Combine(partThumb1, Path.GetFileNameWithoutExtension(file) + "80x80.jpg");

            Debug.WriteLine(partThumb2);
            Debug.WriteLine(File.Exists(partThumb2));
            if (File.Exists(fullPath))
            {
                //delete thumb 
                if (File.Exists(partThumb2))
                {
                    File.Delete(partThumb2);
                }
                File.Delete(fullPath);
                var succesMessage = "Ok";
                return succesMessage;
            }
            var failMessage = "Error Delete";
            return failMessage;
        }
        public JsonFiles GetFileList(int id)
        {

            var r = new List<ViewDataUploadFilesResult>();

            var fullPath = Path.Combine(_storageRoot, id.ToString());
            if (Directory.Exists(fullPath))
            {
                var dir = new DirectoryInfo(fullPath);
                foreach (var file in dir.GetFiles())
                {
                    var sizeInt = unchecked((int)file.Length);
                    r.Add(UploadResult(file.Name, sizeInt, file.FullName, id));
                }

            }
            var files = new JsonFiles(r);

            return files;
        }

        public void UploadAndShowResults(HttpContextBase contentBase, List<ViewDataUploadFilesResult> resultList, int id)
        {
            var httpRequest = contentBase.Request;
            Debug.WriteLine(Directory.Exists(_tempPath));

            var fullPath = Path.Combine(_storageRoot, id.ToString());
            Directory.CreateDirectory(fullPath);
            // Create new folder for thumbs
            Directory.CreateDirectory(fullPath + "/thumbs/");

            foreach (string inputTagName in httpRequest.Files)
            {

                var headers = httpRequest.Headers;

                var file = httpRequest.Files[inputTagName];
                Debug.WriteLine(file.FileName);

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {

                    UploadWholeFile(contentBase, resultList, id);
                }
                else
                {

                    UploadPartialFile(headers["X-File-Name"], contentBase, resultList, id);
                }
            }
        }


        private void UploadWholeFile(HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses, int albumId)
        {

            var request = requestContext.Request;
            for (var i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var pathOnServer = Path.Combine(_storageRoot, albumId.ToString());
                var fullPath = Path.Combine(pathOnServer, Path.GetFileName(file.FileName));
                file.SaveAs(fullPath);

                //Create thumb
                var imageArray = file.FileName.Split('.');
                if (imageArray.Length != 0)
                {
                    var extansion = imageArray[imageArray.Length - 1].ToLower();
                    if (extansion != "jpg" && extansion != "png" && extansion != "jpeg") //Do not create thumb if file is not an image
                    {

                    }
                    else
                    {
                        var thumbfullPath = Path.Combine(pathOnServer, "thumbs");
                        //String fileThumb = file.FileName + ".80x80.jpg";
                        var fileThumb = Path.GetFileNameWithoutExtension(file.FileName) + "80x80.jpg";
                        var thumbfullPath2 = Path.Combine(thumbfullPath, fileThumb);
                        using (var stream = new MemoryStream(File.ReadAllBytes(fullPath)))
                        {
                            var thumbnail = new WebImage(stream).Resize(80, 80);
                            thumbnail.Save(thumbfullPath2, "jpg");
                        }

                    }
                }
                statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, albumId));
            }
        }



        private void UploadPartialFile(string fileName, HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses, int albumId)
        {
            var request = requestContext.Request;
            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            var file = request.Files[0];
            var inputStream = file.InputStream;
            var patchOnServer = Path.Combine(_storageRoot, albumId.ToString());
            var fullName = Path.Combine(patchOnServer, Path.GetFileName(file.FileName));
            var thumbfullPath = Path.Combine(fullName, Path.GetFileName(file.FileName + "80x80.jpg"));
            var handler = new ImageHandler();

            var imageBit = ImageHandler.LoadImage(fullName);
            handler.Save(imageBit, 80, 80, 10, thumbfullPath);
            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }
            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, albumId));
        }
        public ViewDataUploadFilesResult UploadResult(string fileName, int fileSize, string fileFullPath, int albumId)
        {
            var getType = MimeMapping.GetMimeMapping(fileFullPath);
            var result = new ViewDataUploadFilesResult
            {
                Name = fileName,
                Size = fileSize,
                Type = getType,
                Url = _urlBase + albumId + '/' + fileName,
                DeleteUrl = _deleteUrl + "?id=" + albumId + "&file=" + fileName,
                ThumbnailUrl = CheckThumb(getType, fileName, albumId),
                DeleteType = _deleteType
            };
            return result;
        }

        public string CheckThumb(string type, string fileName, int albumId)
        {
            var splited = type.Split('/');
            if (splited.Length == 2)
            {
                var extansion = splited[1].ToLower();
                if (extansion.Equals("jpeg") || extansion.Equals("jpg") || extansion.Equals("png") || extansion.Equals("gif"))
                {
                    var thumbnailUrl = _urlBase + albumId + "/" + "thumbs/" + Path.GetFileNameWithoutExtension(fileName) + "80x80.jpg";
                    return thumbnailUrl;
                }
                else
                {
                    if (extansion.Equals("octet-stream")) //Fix for exe files
                    {
                        return "/Content/Free-file-icons/48px/exe.png";

                    }
                    if (extansion.Contains("zip")) //Fix for exe files
                    {
                        return "/Content/Free-file-icons/48px/zip.png";
                    }
                    var thumbnailUrl = "/Content/Free-file-icons/48px/" + extansion + ".png";
                    return thumbnailUrl;
                }
            }
            return _urlBase + albumId + "/" + "/thumbs/" + Path.GetFileNameWithoutExtension(fileName) + "80x80.jpg";
        }
        public List<string> FilesList()
        {

            var files = new List<string>();
            var path = HostingEnvironment.MapPath(_serverMapPath);
            Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                var di = new DirectoryInfo(path);
                foreach (var fi in di.GetFiles())
                {
                    files.Add(fi.Name);
                    Debug.WriteLine(fi.Name);
                }

            }
            return files;
        }
    }
    public class JsonFiles
    {
        public ViewDataUploadFilesResult[] files { get; set; }
        public string TempFolder { get; set; }

        public JsonFiles(List<ViewDataUploadFilesResult> filesList)
        {
            files = new ViewDataUploadFilesResult[filesList.Count];
            for (var i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }

        }
    }
}