using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace CyberConstructor_RevitAPI_PlayGround
{
    public partial class MainForm : System.Windows.Forms.Form
    {
        UIDocument uidoc;
        Document doc;
        Selection selection;

        public MainForm(UIDocument _uidoc, Document _doc, Selection _selection)
        {
            InitializeComponent();
            uidoc = _uidoc;
            doc = _doc;
            selection = _selection;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<ElementId> eleids = selection.GetElementIds().ToList();
            //很具體地知道我要處理的就是柱子，那我後面的程式在做篩選不就好了嗎

            //我今天就已經很具體地知道我要的是柱子，但是如果我的工具沒有辦法很直覺地幫我選到柱子，那不就GG了嗎8964

            //今天要介紹的就是我的們 -> ISelectionFIlter
            //主程式碼中，我想放入這個篩選的結果，我勢必須要 ISelectionFIlter

            ISelectionFilter filter = new SuperFilter(); // 就是把我們的SuperFiler放進去

            //PickObject

            Element ele = null; //最後要放成果的地方

            Reference refer = uidoc.Selection.PickObject(ObjectType.Element,filter,"Ni Hao Ma"); //如果只是這樣，那就代表你的篩選器根本沒用到

            ele = doc.GetElement(refer);

            MessageBox.Show(ele.Id.ToString());
        }

        //當我們需要一個IselectionFilter 我們需要宣告一個Class來處理
        public class SuperFilter : ISelectionFilter //繼承的觀念
        {
            //ISelectionFilter很重要的觀念 -> Doc看到，他是在選擇過程中的一個篩選條件、動作
            //Revit當中，有什麼樣的東西可以被選呢 -> Element , Reference

            public bool AllowElement(Element ele) //假設我已經很具體地知道我要的是柱子該怎麼辦
            {
                if(ele.Category.Id.IntegerValue == -2001330)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }

            public bool AllowReference(Reference refer , XYZ point) // 今天就先不講Reference 的方式
            {
                return true;
            }

        }

    }
}
