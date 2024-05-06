using System;
using AutoMapper;

namespace eMKParty.BackOffice.Support.Application.Common.Mappings
{
    public interface IMapFrom<T>
    {
        void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
    }
}