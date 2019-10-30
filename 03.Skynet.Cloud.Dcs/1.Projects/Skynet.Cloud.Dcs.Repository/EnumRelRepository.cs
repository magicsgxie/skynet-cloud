using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UWay.Skynet.Cloud.Dcs.Entity;
using UWay.Skynet.Cloud.Data;
using UWay.Skynet.Cloud.Cfgs.Entity;

namespace UWay.Skynet.Cloud.Dcs.Repository
{
    public class EnumRelRepository:ObjectRepository
    {
        

        public EnumRelRepository(IDbContext uow)
            :base(uow)
        {

        }

        public List<Enum_Relation> GetEnumRels()
        {   
            List<Enum_Relation> lstRelation = this.CreateQuery<Enum_Relation>().OrderBy(t => t.OrderBy).ToList();

            foreach (var rel in lstRelation)
            {
                //分拆Redundance_Fields
                rel.Redundance_Fields = new List<DictItem>();
                if (!string.IsNullOrEmpty(rel.RedundanceFieldsString))
                {
                    string[] splitcolumns = rel.RedundanceFieldsString.Replace("||", "$$").Split('|');
                    foreach (string columninfo in splitcolumns)
                        rel.Redundance_Fields.Add(new DictItem() { DictCode = columninfo.Split('/')[0].Replace("$$", "||"), DictName = columninfo.Split('/')[1].Replace("$$", "||") });
                }

                //分拆Hide_Fields
                rel.Hide_Fields = new List<string>();
                if (!string.IsNullOrEmpty(rel.HideFields))
                {
                    string[] splitcolumns = rel.HideFields.Replace("||", "$$").Split('|');
                    foreach (string columninfo in splitcolumns)
                        rel.Hide_Fields.Add(columninfo.Replace("$$", "||"));
                }

                //分拆ExtensionFields
                rel.ExtensionFields = new List<string>();
                if (!string.IsNullOrEmpty(rel.ExtensionFieldsString))
                {
                    string[] splitcolumns = rel.ExtensionFieldsString.Replace("||", "$$").Split('|');
                    foreach (string columninfo in splitcolumns)
                        rel.ExtensionFields.Add(columninfo.Replace("$$", "||"));
                }

                //分拆ExtensionRelationFields
                rel.ExtensionRelationFields = new List<string>();
                if (!string.IsNullOrEmpty(rel.ExtensionRelationFieldsString))
                {
                    string[] splitcolumns = rel.ExtensionRelationFieldsString.Replace("||", "$$").Split('|');
                    foreach (string columninfo in splitcolumns)
                        rel.ExtensionRelationFields.Add(columninfo.Replace("$$", "||"));
                }

            }

            return lstRelation;
        }
    }
}
