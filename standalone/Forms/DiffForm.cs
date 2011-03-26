using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AwkEverywhere.Frontend;

namespace AwkEverywhere.Forms
{
    public partial class DiffForm : Form
    {
        public DiffForm(DiffFrontEnd frontEnd)
        {
            InitializeComponent();
            this.moFrontEnd = frontEnd;
        }

        private DiffFrontEnd moFrontEnd;
        private List<DiffElt> mDiffList = new List<DiffElt>();
        private int mCurrentDiffElt = 0;

        private string mCurrentData1;
        private string mCurrentData2;

        public bool ShowDiff(string data1, string data2)
        {
            mCurrentData1 = data1;
            mCurrentData2 = data2;
            

            moFrontEnd.Data.Clear();
            moFrontEnd.Data.Add(data1.TrimEnd());
            moFrontEnd.Data.Add(data2.TrimEnd());
            moFrontEnd.ExecScript();

            string sResult = moFrontEnd.Result;
            string sError = moFrontEnd.Error;
            if (sError.Trim() != string.Empty)
            {
                MessageBox.Show(sError);
                return false;
            }
            else
            {
                TB_Input.SuspendLayout();
                TB_Output.SuspendLayout();
                this.ShowDiff(data1, data2, moFrontEnd.Result);

                TB_Input.ResumeLayout();
                TB_Output.ResumeLayout();
            }
            
            
            return true;
        }

        private void ShowDiff(string data1, string data2, string diffResult)
        {
            TB_Input.Text = string.Empty;
            TB_Output.Text = string.Empty;
            mCurrentDiffElt = 0;
            mDiffList.Clear();

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
                            //selstart1 += TB_Output.Lines[TB_Input.Lines.Count()].Length + 1;
                            selstart1 += TB_Input.Lines[TB_Input.Lines.Count()-2].Length + 1;
                        }
                        for (int i = 0; i < (end2 - start2 + 1);i++ )
                        {
                            string text = new string(' ', TB_Output.Lines[Math.Max(0,TB_Input.Lines.Count()-1)].Length)+Environment.NewLine;
                            pos1+=text.Length-1;
                            TB_Input.AppendText(text);
                        }
                        color = Color.LightBlue;
                        break;
                    case 'd' :
                        if (TB_Output.Lines.Count() > 0)
                        {
                            //selstart2 += TB_Input.Lines[TB_Output.Lines.Count()].Length +1;
                            selstart2 += TB_Output.Lines[TB_Output.Lines.Count()-2].Length + 1;
                        }
                        for (int i = 0; i < (end1 - start1 + 1); i++)
                        {
                            string text = new string(' ', TB_Input.Lines[Math.Max(0,TB_Output.Lines.Count()-1)].Length)+Environment.NewLine;
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
                                string text = new string(' ', TB_Input.Lines[Math.Max(0,TB_Output.Lines.Count()-1)].Length) + Environment.NewLine;
                                pos2 += text.Length-1;
                                TB_Output.AppendText(text);
                            }
                        }
                        else if (t2 > t1)
                        {

                            //selstart1 += TB_Output.Lines[TB_Input.Lines.Count() - 1].Length;
                            for (int i = 0; i < t2 - t1; i++)
                            {
                                string text = new string(' ', TB_Output.Lines[Math.Max(0,TB_Input.Lines.Count()-1)].Length) + Environment.NewLine;
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

                mDiffList.Add(new DiffElt(selstart1, pos1 - selstart1, selstart2, pos2 - selstart2));

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
                                TB_Input.SelectionColor = Color.White;
                                TB_Input.SelectionBackColor = Color.Green;

                                TB_Output.Select(selstart2 + j, 1);
                                TB_Output.SelectionColor = Color.White;
                                TB_Output.SelectionBackColor = Color.Green;
                            }
                        }
                        else
                        {
                            TB_Input.Select(selstart1 + j, 1);
                            TB_Input.SelectionColor = Color.White;
                            TB_Input.SelectionBackColor = Color.Green;
                        }
                    }
                    if (sel2.Length > sel1.Length)
                    {
                        TB_Output.Select(selstart2 + sel1.Length, sel2.Length-sel1.Length);
                        TB_Output.SelectionColor = Color.White;
                        TB_Output.SelectionBackColor = Color.Green;
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

            if (mDiffList.Count > 0)
            {
                DiffElt elt = mDiffList[0];
                TB_Input.Select(elt.SelStart1, elt.SelLength1);
                TB_Output.Select(elt.SelStart2, elt.SelLength2);
                TB_Input.SelectionBullet = true;
                TB_Output.SelectionBullet = true;
            }
        }

        private void DiffForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Hide();
            e.Cancel = true;
        }

        private class DiffElt
        {
            public DiffElt(int selStart1, int selLength1, int selStart2, int selLength2)
            {
                SelStart1 = selStart1;
                SelLength1 = selLength1;

                SelStart2 = selStart2;
                SelLength2 = selLength2;
            }

            public int SelStart1 { get; set; }
            public int SelLength1 { get; set; }

            public int SelStart2 { get; set; }
            public int SelLength2 { get; set; }
        }

        private void BtnNextDiff_Click(object sender, EventArgs e)
        {
            if (mDiffList.Count > 0)
            {
                if (mCurrentDiffElt < mDiffList.Count - 1)
                {
                    DiffElt elt = mDiffList[mCurrentDiffElt];
                    TB_Input.Select(elt.SelStart1, elt.SelLength1);
                    TB_Output.Select(elt.SelStart2, elt.SelLength2);
                    TB_Input.SelectionBullet = false;
                    TB_Output.SelectionBullet = false;


                    mCurrentDiffElt++;

                    elt = mDiffList[mCurrentDiffElt];
                    TB_Input.Select(elt.SelStart1, elt.SelLength1);
                    TB_Output.Select(elt.SelStart2, elt.SelLength2);
                    TB_Input.SelectionBullet = true;
                    TB_Output.SelectionBullet = true;

                    TB_Output.ScrollToCaret();
                    TB_Input.ScrollToCaret();
                }
            }
        }

        private void BtnPrevDiff_Click(object sender, EventArgs e)
        {
            if (mCurrentDiffElt > 0)
            {
                DiffElt elt = mDiffList[mCurrentDiffElt];
                TB_Input.Select(elt.SelStart1, elt.SelLength1);
                TB_Output.Select(elt.SelStart2, elt.SelLength2);
                TB_Input.SelectionBullet = false;
                TB_Output.SelectionBullet = false;


                mCurrentDiffElt--;

                elt = mDiffList[mCurrentDiffElt];
                TB_Input.Select(elt.SelStart1, elt.SelLength1);
                TB_Output.Select(elt.SelStart2, elt.SelLength2);
                TB_Input.SelectionBullet = true;
                TB_Output.SelectionBullet = true;

                TB_Output.ScrollToCaret();
                TB_Input.ScrollToCaret();
            }
        }

        private void ChkIgnoreCase_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkIgnoreCase.Checked)
            {
                moFrontEnd.Args.Add("-i");
            }
            else
            {
                moFrontEnd.Args.Remove("-i");
            }
            ShowDiff(mCurrentData1, mCurrentData2);
        }

        private void ChkMinimal_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkMinimal.Checked)
            {
                moFrontEnd.Args.Add("-d");
            }
            else
            {
                moFrontEnd.Args.Remove("-d");
            }
            ShowDiff(mCurrentData1, mCurrentData2);
        }

    }
}
