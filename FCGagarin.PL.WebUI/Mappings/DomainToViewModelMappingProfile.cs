﻿using AutoMapper;
using FCGagarin.DAL.Entities;
using FCGagarin.PL.ViewModels;

namespace FCGagarin.PL.WebUI.Mappings
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public override string ProfileName
        {
            get
            {
                return "DomainToViewModelMappings";
            }
        }

        public DomainToViewModelMappingProfile()
        {
            CreateMap<News, NewsViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(source => source.Author.ToString()));
            CreateMap<News, NewsFormModel>();

            CreateMap<VideoAlbum, VideoAlbumViewModel>();
            CreateMap<VideoAlbum, VideoAlbumFormModel>();
            CreateMap<VideoAlbum, VideoAlbumDetailsViewModel>()
                .ForMember(vadbm => vadbm.VideoAlbumViewModel, opt => opt.MapFrom(source => source))
                .ForMember(vadbm => vadbm.VideoViewModelList, opt => opt.MapFrom(source => source.Videos));

            CreateMap<Video, VideoViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(source => source.Author.ToString()));
            CreateMap<Video, VideoFormModel>().ForMember(x=>x.AlbumName, opt => opt.MapFrom(source => source.Album.Name));

            CreateMap<PhotoAlbum, PhotoAlbumViewModel>();
            CreateMap<PhotoAlbum, PhotoAlbumFormModel>();
            CreateMap<PhotoAlbum, PhotoAlbumDetailsViewModel>()
                .ForMember(padbm => padbm.PhotoAlbumViewModel, opt => opt.MapFrom(source => source));

            CreateMap<Photo, PhotoViewModel>().ForMember(x => x.Author, opt => opt.MapFrom(source => source.Author.ToString()));
            CreateMap<Photo, PhotoFormModel>().ForMember(x => x.AlbumName, opt => opt.MapFrom(source => source.Album.Name));
        }
    }
}