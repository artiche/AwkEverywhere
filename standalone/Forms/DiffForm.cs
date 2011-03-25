using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AwkEverywhere.Forms
{
    public partial class DiffForm : Form
    {
        public DiffForm()
        {
            InitializeComponent();
        }

        public void ShowDiff(string data1, string data2, string diffResult)
        {
            TB_Input.Text = string.Empty;
            TB_Output.Text = string.Empty;

            string[] tsData1 = data1.Split('\n');
            string[] tsData2 = data2.Split('\n');
            string[] tsDiffResult = diffResult.Split('\n');

            int iDiff = 0;
            int iData1 = 0;
            int iData2 = 0;

            int pos1 = 0;
            int pos2 = 0;

            while (iDiff < tsDiffResult.Length)
            {
                string item = tsDiffResult[iDiff++].Trim();
                if (item.StartsWith(">")
                    || item.StartsWith("<")
                    || item.StartsWith("-")
                    || item.StartsWith(@"\")
                    || item == string.Empty)
                {
                    continue;
                }

                int start1 = -1;
                int end1 = -1;
                int start2 = -1;
                int end2 = -1;
                char diffCase = ' ';

                if (item.IndexOf('a') > -1)
                {
                    //new lines in data2
                    diffCase = 'a';
                }

                if (item.IndexOf('d') > -1)
                {
                    //lines deleted in data2
                    diffCase = 'd';
                }

                if (item.IndexOf('c') > -1)
                {
                    //lines changed between data1 and data2
                    diffCase = 'c';
                }

                string[] tabTmp = item.Split(diffCase);

                string[] tab1 = tabTmp[0].Split(',');
                start1 = Int32.Parse(tab1[0]);
                end1 = start1;
                if (tab1.Length > 1)
                {
                    end1 = Int32.Parse(tab1[1]);
                }

                string[] tab2 = tabTmp[1].Split(',');
                start2 = Int32.Parse(tab2[0]);
                end2 = start2;
                if (tab2.Length > 1)
                {
                    end2 = Int32.Parse(tab2[1]);
                }

                int selstart1 = 0;
                while (iData1 < end1)
                {
                    if (iData1 == start1-1)
                    {
                        selstart1 = pos1;
                    }
                    pos1 += tsData1[iData1].Length + 1;
                    TB_Input.AppendText(tsData1[iData1++] + Environment.NewLine);
                    
                }

                int selstart2 = 0;
                while (iData2 < end2)
                {
                    if (iData2 == start2 - 1)
                    {
                        selstart2 = pos2;
                    }
                    pos2 += tsData2[iData2].Length + 1;
                    TB_Output.AppendText(tsData2[iData2++] + Environment.NewLine);
                }
                
                Color color = Color.White;
                switch (diffCase)
                {
                    case 'a' :
                        if (TB_Input.Lines.Count() > 0)
                        {
                            selstart1 += TB_Output.Lines[TB_Input.Lines.Count() - 1].Length;
                        }
                        for (int i = 0; i < (end2 - start2 + 1);i++ )
                        {
                            string text = new string(' ', TB_Output.Lines[Math.Max(0,TB_Input.Lines.Count())].Length)+Environment.NewLine;
                            pos1+=text.Length-1;
                            TB_Input.AppendText(text);
                        }
                        color = Color.LightBlue;
                        break;
                    case 'd' :
                        if (TB_Output.Lines.Count() > 0)
                        {
                            selstart2 += TB_Input.Lines[TB_Output.Lines.Count() - 1].Length;
                        }
                        for (int i = 0; i < (end1 - start1 + 1); i++)
                        {
                            string text = new string(' ', TB_Input.Lines[Math.Max(0,TB_Output.Lines.Count())].Length)+Environment.NewLine;
                            pos2+=text.Length-1;
                            TB_Output.AppendText(text);
                        }
                        color = Color.LightPink;
                        break;
                    case 'c' :
                        int t1 = end1 - start1 + 1;
                        int t2 = end2 - start2 + 1;
                        if (t1 > t2)
                        {
                            //selstart2 += TB_Input.Lines[TB_Output.Lines.Count() - 1].Length;
                            for (int i = 0; i < t1 - t2; i++)
                            {
                                string text = new string(' ', TB_Input.Lines[Math.Max(0,TB_Output.Lines.Count())].Length) + Environment.NewLine;
                                pos2 += text.Length-1;
                                TB_Output.AppendText(text);
                            }
                        }
                        else if (t2 > t1)
                        {

                            //selstart1 += TB_Output.Lines[TB_Input.Lines.Count() - 1].Length;
                            for (int i = 0; i < t2 - t1; i++)
                            {
                                string text = new string(' ', TB_Output.Lines[Math.Max(0,TB_Input.Lines.Count())].Length) + Environment.NewLine;
                                pos1 += text.Length-1;
                                TB_Input.AppendText(text);
                            }
                        }
                        color = Color.LightGreen;


                        break;
                }

                TB_Input.Select(selstart1, pos1 - selstart1);
                TB_Input.SelectionBackColor = color;

                TB_Output.Select(selstart2, pos2 - selstart2);
                TB_Output.SelectionBackColor = color;

                if (diffCase == 'c')
                {
                    int localSelStart1 = selstart1;
                    int localSelStart2 = selstart2;

                    string sel1 = TB_Input.SelectedText;
                    string sel2 = TB_Output.SelectedText;

                    for (int j = 0; j < sel1.Length; j++)
                    {
                        if (j < sel2.Length)
                        {
                            if (sel1[j] != sel2[j])
                            {
                                TB_Input.Select(selstart1 + j, 1);
                                TB_Input.SelectionColor = Color.Red;

                                TB_Output.Select(selstart2 + j, 1);
                                TB_Output.SelectionColor = Color.Red;
                            }
                        }
                        else
                        {
                            TB_Input.Select(selstart1 + j, 1);
                            TB_Input.SelectionColor = Color.Red;
                        }
                    }
                    if (sel2.Length > sel1.Length)
                    {
                        TB_Output.Select(selstart2 + sel1.Length, sel2.Length-sel1.Length);
                        TB_Output.SelectionColor = Color.Red;
                    }
                }

            }

            for (int j = iData1; j < tsData1.Length; j++)
            {
                TB_Input.AppendText(tsData1[j]+Environment.NewLine);
            }

            for (int j = iData2; j < tsData2.Length; j++)
            {
                TB_Output.AppendText(tsData2[j] + Environment.NewLine);
            }
        }

        private void DiffForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }
    }
}
