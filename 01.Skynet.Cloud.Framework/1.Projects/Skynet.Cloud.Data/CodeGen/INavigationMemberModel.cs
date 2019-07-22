using System;
using System.Collections.Generic;
using System.Text;
using UWay.Skynet.Cloud.Data.Schema;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    public interface INavigationMemberModel
    {
        bool IsManyToOne { get; }
        string DeclareTypeName { get; }
        string MemberName { get; }
        string OtherKeyMemberName { get; }
        IRelationSchema Relation { get; }
    }
}
