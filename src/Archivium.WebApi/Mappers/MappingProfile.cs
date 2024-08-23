using Archivium.Domain.Entities.Categories;
using Archivium.Domain.Entities.Collections;
using Archivium.Domain.Entities.Commets;
using Archivium.Domain.Entities.Commons;
using Archivium.Domain.Entities.Fields;
using Archivium.Domain.Entities.Items;
using Archivium.Domain.Entities.Likes;
using Archivium.Domain.Entities.Tags;
using Archivium.Domain.Entities.Users;
using Archivium.WebApi.Models.Assets;
using Archivium.WebApi.Models.Categories;
using Archivium.WebApi.Models.Collections;
using Archivium.WebApi.Models.Comments;
using Archivium.WebApi.Models.Fields;
using Archivium.WebApi.Models.FieldValues;
using Archivium.WebApi.Models.Items;
using Archivium.WebApi.Models.ItemTags;
using Archivium.WebApi.Models.Likes;
using Archivium.WebApi.Models.Tags;
using Archivium.WebApi.Models.Users;
using AutoMapper;

namespace Archivium.WebApi.Mappers;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<AssetViewModel, Asset>().ReverseMap();

        CreateMap<UserViewModel, User>().ReverseMap();
        CreateMap<UserLoginViewModel, User>().ReverseMap();
        CreateMap<User, UserCreateModel>().ReverseMap();
        CreateMap<User, UserUpdateModel>().ReverseMap();

        CreateMap<LikeViewModel, Like>().ReverseMap();
        CreateMap<Like, LikeCreateModel>().ReverseMap();

        CreateMap<ItemTagViewModel, ItemTag>().ReverseMap();
        CreateMap<ItemTag, ItemTagCreateModel>().ReverseMap();
        CreateMap<ItemTag, ItemTagUpdateModel>().ReverseMap();

        CreateMap<TagViewModel, Tag>().ReverseMap();
        CreateMap<Tag, TagCreateModel>().ReverseMap();
        CreateMap<Tag, TagUpdateModel>().ReverseMap();

        CreateMap<ItemViewModel, Item>().ReverseMap();
        CreateMap<Item, ItemCreateModel>().ReverseMap();
        CreateMap<Item, ItemUpdateModel>().ReverseMap();

        CreateMap<FieldViewModel, Field>().ReverseMap();
        CreateMap<Field, FieldCreateModel>().ReverseMap();
        CreateMap<Field, FieldUpdateModel>().ReverseMap();

        CreateMap<FieldValueViewModel, FieldValue>().ReverseMap();
        CreateMap<FieldValue, FieldValueCreateModel>().ReverseMap();
        CreateMap<FieldValue, FieldValueUpdateModel>().ReverseMap();

        CreateMap<CommentViewModel, Comment>().ReverseMap();
        CreateMap<Comment, CommentCreateModel>().ReverseMap();
        CreateMap<Comment, CommentUpdateModel>().ReverseMap();

        CreateMap<CollectionViewModel, Collection>().ReverseMap();
        CreateMap<Collection, CollectionCreateModel>().ReverseMap();
        CreateMap<Collection, CollectionUpdateModel>().ReverseMap();

        CreateMap<CategoryViewModel, Category>().ReverseMap();
        CreateMap<Category, CategoryCreateModel>().ReverseMap();
        CreateMap<Category, CategoryUpdateModel>().ReverseMap();
    }
}

