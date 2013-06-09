/*
 * Created by SharpDevelop.
 * User: s4
 * Date: 2013/06/08
 * Time: 21:06
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Windows.Forms;
using System.Drawing;

namespace ss
{
	/// <summary>
	/// Description of DataGridCustom.
	/// </summary>
	//DataGridTextBoxColumnを継承してクラスを作成
public class MyDataGridTextBoxColumn : DataGridTextBoxColumn
{
    //Paintメソッドをオーバーライドする
    protected override void Paint(Graphics g,
        Rectangle bounds,
        CurrencyManager source,
        int rowNum, Brush backBrush,
        Brush foreBrush,
        bool alignToRight)
    {
        //セルの値を取得する
        object cellValue =
            this.GetColumnValueAtRow(source, rowNum);
        if (cellValue != null)
        {
            //値が"0"のセルの前景色と背景色を変える
            if (cellValue != DBNull.Value &&(string) cellValue == "0")
            {
                foreBrush = new SolidBrush(Color.White);
                backBrush = new SolidBrush(Color.Black);
            }
        }
        //基本クラスのPaintメソッドを呼び出す
        base.Paint(g, bounds, source, rowNum,
            backBrush, foreBrush, alignToRight);
    }
}
}
