using System;
using System.Collections.Generic;
using UWay.Skynet.Cloud.Reflection;
using System.Reflection;
using UWay.Skynet.Cloud.ExceptionHandle;

using UWay.Skynet.Cloud;

namespace UWay.Skynet.Cloud.Mapping.Internal
{

    abstract class MapperBase : IMapper
    {

        protected void FilterMembers(ref object from, ref object to)
        {
            foreach (var item in _Info.memberMappings)
                item.Key(to, item.Value.Delegate.DynamicInvoke(from));
        }

        protected readonly string Key;
        internal MapperInfo _Info;
        public IErrorState State { get; internal set; }

        protected MapperBase(Type fromType, Type toType)
        {
            Key = fromType.FullName + "->" + toType.FullName;
            _Info = new MapperInfo
            {
                From = fromType
                ,
                To = toType
                ,
                Key = this.Key
            };
        }

        internal static bool IsPrimitiveTypeMapping(Type fromType, Type toType)
        {
            if (fromType == null)
                return false;
            if (toType == null)
                return false;
            if (fromType.IsNullable())
                fromType = fromType.GetGenericArguments()[0];
            if (Type.GetTypeCode(fromType) == TypeCode.Object)
                return false;
            if (toType.IsNullable())
                toType = toType.GetGenericArguments()[0];
            if (Type.GetTypeCode(toType) == TypeCode.Object)
                return false;

            return true;
        }

        public IMapperInfo Info { get { return _Info; } }

        public abstract void Map(ref object from, ref object to);

        protected void AddErrorState(string member, Exception ex)
        {
            State.AddError(member, ex.GetExceptionMessage());
        }


        IMapperInfo IMapper.Info
        {
            get { throw new NotImplementedException(); }
        }

        IErrorState IMapper.State
        {
            get { throw new NotImplementedException(); }
        }

        void IMapper.Map(ref object from, ref object to)
        {
            throw new NotImplementedException();
        }

        object IMapper.Map(object from, Type fromType, Type toType)
        {
            throw new NotImplementedException();
        }
    }

    internal static class Error
    {
        public static string GetExceptionMessage(this Exception ex)
        {
            string message = null;
            while (ex != null)
            {
                message = ex.Message;
                ex = ex.InnerException;
            }
            return message;
        }

        public static Exception GetInnerException(this Exception ex)
        {
            Exception innerEx = ex;
            while (true)
            {
                if (innerEx.InnerException == null)
                    return innerEx;
                innerEx = innerEx.InnerException;
            }

        }
    }
}
