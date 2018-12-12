using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreV2.PLANLAMA.DESIGNER.CLASS
{
    class TreeListSelect
    {
    }

    //public class PrintNodesOperation : TreeListOperation
    //{

    //    private ListBoxControl _ListBox;
    //    public PrintNodesOperation(ListBoxControl listBox)
    //    {
    //        _ListBox = listBox;

    //    }

    //    public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
    //    {
    //        PrintNode(node);
    //    }

    //    private void PrintNode(DevExpress.XtraTreeList.Nodes.TreeListNode node)
    //    {
    //        string nodeAsString = NodeToString(node);
    //        _ListBox.Items.Add(nodeAsString);
    //    }
    //    private string NodeToString(DevExpress.XtraTreeList.Nodes.TreeListNode node)
    //    {
    //        string res = string.Empty;
    //        string indent = "    ";
    //        for (int i = 0; i < node.Level; i++)
    //        {
    //            res += indent;
    //        }
    //        if (node.HasChildren)
    //            res += "+";
    //        else
    //            res += indent;
    //        res += "|";
    //        foreach (TreeListColumn col in node.TreeList.VisibleColumns)
    //        {
    //            res += String.Format("{0};", node.GetDisplayText(col));
    //        }
    //        res += "|" + Environment.NewLine;
    //        return res;
    //    }
    //}
}
