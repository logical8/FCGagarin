﻿namespace FCGagarin.PL.ViewModels
{
    public class FilesViewModel
    {
        public FilesViewModel()
        {
            files = new ViewDataUploadFilesResult[0];
        }

        public ViewDataUploadFilesResult[] files { get; set; }
    }

    public class ViewDataUploadFilesResult
    {
        public string Name { get; set; }
        public int Size { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
        public string DeleteUrl { get; set; }
        public string ThumbnailUrl { get; set; }
        public string DeleteType { get; set; }
    }
}