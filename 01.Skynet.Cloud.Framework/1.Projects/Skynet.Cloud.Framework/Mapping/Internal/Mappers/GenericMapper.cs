using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;
using UWay.Skynet.Cloud.Reflection;
using System.Diagnostics;
using UWay.Skynet.Cloud.ExceptionHandle;

namespace UWay.Skynet.Cloud.Mapping.Internal
{
    sealed class GenericMapper<TFrom, TTo> : IMapper<TFrom, TTo>, IMapper
    {
        List<Action<TFrom, TTo>> beforeMapHandlers;
        List<Action<TFrom, TTo>> afterMapHandlers;

        readonly MapperBase InnerMapper;
        public GenericMapper(MapperBase innerMapper)
        {
            InnerMapper = innerMapper;
        }

        #region IMapper<TFrom,TTo> Members


        public IMapper<TFrom, TTo> IgnoreCase(bool flag)
        {
            InnerMapper._Info.IgnoreCase = flag;
            return this;
        }

        public IMapper<TFrom, TTo> IgnoreUnderscore(bool flag)
        {
            InnerMapper._Info.IgnoreUnderscore = flag;
            return this;
        }

        public IMapper<TFrom, TTo> IgnoreSourceMembers(Func<Type, IEnumerable<string>> filter)
        {
            Guard.NotNull(filter, "filter");

            if (InnerMapper._Info.ignoreSourceMembers == null)
                InnerMapper._Info.ignoreSourceMembers = new HashSet<string>();

            foreach (var item in filter(InnerMapper._Info.From))
                InnerMapper._Info.ignoreSourceMembers.Add(item);
            return this;
        }


        public IMapper<TFrom, TTo> IgnoreDestinationMembers(Func<Type, IEnumerable<string>> filter)
        {
            Guard.NotNull(filter, "filter");

            if (InnerMapper._Info.ignoreDestinationMembers == null)
                InnerMapper._Info.ignoreDestinationMembers = new HashSet<string>();

            foreach (var item in filter(InnerMapper._Info.To))
                InnerMapper._Info.ignoreDestinationMembers.Add(item);
            return this;
        }

        public IMapper<TFrom, TTo> IgnoreSourceMember(Expression<Func<TFrom, object>> member)
        {
            Guard.NotNull(member, "member");
            var memberInfo = member.FindMember();

            if (InnerMapper._Info.ignoreSourceMembers == null)
                InnerMapper._Info.ignoreSourceMembers = new HashSet<string>();

            InnerMapper._Info.ignoreSourceMembers.Add(memberInfo.Name);
            return this;
        }

        public IMapper<TFrom, TTo> IgnoreDestinationMember(Expression<Func<TTo, object>> member)
        {
            Guard.NotNull(member, "member");
            var memberInfo = member.FindMember();

            if (InnerMapper._Info.ignoreDestinationMembers == null)
                InnerMapper._Info.ignoreDestinationMembers = new HashSet<string>();

            InnerMapper._Info.ignoreDestinationMembers.Add(memberInfo.Name);
            return this;
        }

        public IMapper<TFrom, TTo> ForMember(Expression<Func<TTo, object>> destinationMember,
                                                                   Func<TFrom, object> memberOptions)
        {
            Guard.NotNull(destinationMember, "destinationMember");
            Guard.NotNull(memberOptions, "memberOptions");

            var memberInfo = destinationMember.FindMember();

            if (InnerMapper._Info.memberMappings == null)
                InnerMapper._Info.memberMappings = new Dictionary<Setter, MemberSetterInfo>();

            InnerMapper._Info.memberMappings.Add(memberInfo.GetSetter(), new MemberSetterInfo { MemberName = memberInfo.Name, Delegate = memberOptions });

            return this;
        }

        public IMapper<TFrom, TTo> MatchMembers(Func<string, string, bool> membersMatcher)
        {
            Guard.NotNull(membersMatcher, "membersMatcher");
            InnerMapper._Info.membersMatcher = membersMatcher;
            return this;
        }

        public IMapper<TFrom, TTo> ConvertUsing<From, To>(Func<From, To> converter)
        {
            Guard.NotNull(converter, "converter");

            var key = typeof(From).FullName + "->" + typeof(To).FullName;

            if (InnerMapper._Info.converters == null)
                InnerMapper._Info.converters = new Dictionary<string, Delegate>();

            if (!InnerMapper._Info.converters.ContainsKey(key))
                InnerMapper._Info.converters.Add(key, converter);

            return this;
        }

        public IMapper<TFrom, TTo> BeforeMap(Action<TFrom, TTo> handler)
        {
            if (beforeMapHandlers == null)
                beforeMapHandlers = new List<Action<TFrom, TTo>>(1);
            beforeMapHandlers.Add(handler);
            return this;
        }

        public IMapper<TFrom, TTo> AfterMap(Action<TFrom, TTo> handler)
        {
            if (afterMapHandlers == null)
                afterMapHandlers = new List<Action<TFrom, TTo>>(1);
            afterMapHandlers.Add(handler);
            return this;
        }

        #endregion

        public IMapperInfo Info { get { return InnerMapper._Info; } }
        public IErrorState State { get { return InnerMapper.State; } }

        void IMapper.Map(ref object from, ref object to)
        {
            InnerMapper.Map(ref from, ref to);
        }

        public TTo Map(TFrom from)
        {
            var to = default(TTo);
            InternalMap(ref from, ref to);
            return to;
        }

        public void Map(TFrom from, ref TTo to)
        {
            InternalMap(ref from, ref to);
        }

        public object Map(object from, Type fromType, Type toType)
        {
            TFrom tmpFrom = (TFrom)from;
            TTo tmpTo = default(TTo);

            InternalMap(ref tmpFrom, ref tmpTo);

            return tmpTo;
        }

        private void InternalMap(ref TFrom from, ref TTo to)
        {
            var fromType = InnerMapper._Info.From;
            var toType = InnerMapper._Info.To;

            object tmpFrom = from;
            object tmpTo = to;

            if (Mapper.EnableErrorState)
                InnerMapper.State = new ErrorState();

            if (beforeMapHandlers != null)
                foreach (var item in beforeMapHandlers)
                    item(from, to);

            InnerMapper.Map(ref tmpFrom, ref tmpTo);

            from = (TFrom)tmpFrom;
            to = (TTo)tmpTo;

            if (afterMapHandlers != null)
                foreach (var item in afterMapHandlers)
                    item(from, to);
        }
    }
}
