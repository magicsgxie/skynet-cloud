// ======================================================================
// 
//           filename : BookInfo.cs
//           description :
// 
//           created by magic.s.g.xie at  -- 
//           
//           
//           
//           
// 
// ======================================================================

namespace UWay.Skynet.Cloud.IE.Tests.Models.Export
{
    /// <summary>
    /// 教材信息
    /// </summary>
    public class BookInfo
    {
        public int RowNo { get; }
        public string No { get; }
        public string Name { get; }
        public string EditorInChief { get; }
        public string PublishingHouse { get; }
        public string Price { get; }
        public int PurchaseQuantity { get; }
        public string Remark { get; }

        public BookInfo(int rowNo, string no, string name, string editorInChief, string publishingHouse, string price, int purchaseQuantity, string remark)
        {
            RowNo = rowNo;
            No = no;
            Name = name;
            EditorInChief = editorInChief;
            PublishingHouse = publishingHouse;
            Price = price;
            PurchaseQuantity = purchaseQuantity;
            Remark = remark;
        }
    }
}