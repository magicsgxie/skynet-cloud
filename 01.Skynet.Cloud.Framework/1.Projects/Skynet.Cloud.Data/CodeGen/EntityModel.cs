using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UWay.Skynet.Cloud.Data.Schema;

namespace UWay.Skynet.Cloud.Data.CodeGen
{
    class EntityModel : IEntityModel
    {
        public ITableSchema Table { get; set; }
        public string ClassName { get; set; }
        public string QueryableName { get; set; }

        internal List<IMemberModel> members = new List<IMemberModel>();
        internal List<INavigationMemberModel> manyToOnes = new List<INavigationMemberModel>();
        internal List<INavigationMemberModel> oneToMany = new List<INavigationMemberModel>();

        public IMemberModel[] PrimaryKeys { get { return members.Where(p => p.Column.IsPrimaryKey).ToArray(); } }
        public IMemberModel[] Members { get { return members.Where(p => !p.Column.IsPrimaryKey).ToArray(); } }
        public INavigationMemberModel[] ManyToOnes { get { return manyToOnes.ToArray(); } }
        public INavigationMemberModel[] OneToMany { get { return oneToMany.ToArray(); } }
    }

    class MemberModel : IMemberModel
    {
        public string MemberName { get; set; }
        public IColumnSchema Column { get; set; }
    }

    class NavigationMemberModel : INavigationMemberModel
    {
        public bool IsManyToOne { get; set; }
        public string MemberName { get; set; }
        public string DeclareTypeName { get; set; }
        public IRelationSchema Relation { get; set; }


        public string OtherKeyMemberName
        {
            get { throw new NotImplementedException(); }
        }
    }
}
