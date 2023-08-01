using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

using DataAccess;
using Ronaldo.common;

public partial class Config_RaceConfig : Ronaldo.uibase.PageBase
{
    protected override void LoadData()
    {
        base.LoadData();

        PageDataSource = DBConn.RunStoreProcedure(Constants.SP_GETCONFIG, new string[] { "@type" }, new object[] { Constants.GAMETYPE_RACE });
    }

    protected override void BindData()
    {
        base.BindData();

        System.Collections.Generic.Dictionary<string, string> dicConfigs = new System.Collections.Generic.Dictionary<string, string>();
        for (int i = 0; i < DataSetUtil.RowCount(PageDataSource); i++)
            dicConfigs.Add(DataSetUtil.RowStringValue(PageDataSource, "conf_name", i), DataSetUtil.RowStringValue(PageDataSource, "conf_value", i));

        string[] strTmp = null;

        rblGameResult.SelectedValue = dicConfigs["race_result_mode"];
        tbxRatio1_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_1"]);
        strTmp = dicConfigs["race_betratio_2"].Split(';');
        tbxRatio2_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio2_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_3"].Split(';');
        tbxRatio3_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio3_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_4"].Split(';');
        tbxRatio4_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio4_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_5"].Split(';');
        tbxRatio5_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio5_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_11"].Split(';');
        tbxRatio11_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio11_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio11_3.Text = string.Format("{0:F2}", strTmp[2]);
        tbxRatio11_4.Text = string.Format("{0:F2}", strTmp[3]);
        tbxRatio11_5.Text = string.Format("{0:F2}", strTmp[4]);
        tbxRatio11_6.Text = string.Format("{0:F2}", strTmp[5]);
        tbxRatio11_7.Text = string.Format("{0:F2}", strTmp[6]);
        tbxRatio11_8.Text = string.Format("{0:F2}", strTmp[7]);
        tbxRatio11_9.Text = string.Format("{0:F2}", strTmp[8]);
        tbxRatio11_10.Text = string.Format("{0:F2}", strTmp[9]);
        tbxRatio11_11.Text = string.Format("{0:F2}", strTmp[10]);
        tbxRatio11_12.Text = string.Format("{0:F2}", strTmp[11]);
        tbxRatio11_13.Text = string.Format("{0:F2}", strTmp[12]);
        tbxRatio11_14.Text = string.Format("{0:F2}", strTmp[13]);
        tbxRatio11_15.Text = string.Format("{0:F2}", strTmp[14]);
        tbxRatio11_16.Text = string.Format("{0:F2}", strTmp[15]);
        tbxRatio11_17.Text = string.Format("{0:F2}", strTmp[16]);
        tbxRatio11_18.Text = string.Format("{0:F2}", strTmp[17]);
        tbxRatio11_19.Text = string.Format("{0:F2}", strTmp[18]);
        tbxRatio11_20.Text = string.Format("{0:F2}", strTmp[19]);
        tbxRatio11_21.Text = string.Format("{0:F2}", strTmp[20]);
        tbxRatio11_22.Text = string.Format("{0:F2}", strTmp[21]);
        tbxRatio11_23.Text = string.Format("{0:F2}", strTmp[22]);
        tbxRatio11_24.Text = string.Format("{0:F2}", strTmp[23]);
        tbxRatio11_25.Text = string.Format("{0:F2}", strTmp[24]);
        tbxRatio11_26.Text = string.Format("{0:F2}", strTmp[25]);
        tbxRatio12_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_12"]);
        strTmp = dicConfigs["race_betratio_13"].Split(';');
        tbxRatio13_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio13_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio13_3.Text = string.Format("{0:F2}", strTmp[2]);
        tbxRatio13_4.Text = string.Format("{0:F2}", strTmp[3]);
        tbxRatio13_5.Text = string.Format("{0:F2}", strTmp[4]);
        strTmp = dicConfigs["race_betratio_14"].Split(';');
        tbxRatio14_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio14_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio15_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_15"]);
        tbxRatio16_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_16"]);

        tbxRatio_M_1_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_m_1"]);
        strTmp = dicConfigs["race_betratio_m_2"].Split(';');
        tbxRatio_M_2_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_2_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_m_3"].Split(';');
        tbxRatio_M_3_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_3_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_m_4"].Split(';');
        tbxRatio_M_4_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_4_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_m_5"].Split(';');
        tbxRatio_M_5_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_5_2.Text = string.Format("{0:F2}", strTmp[1]);
        strTmp = dicConfigs["race_betratio_m_11"].Split(';');
        tbxRatio_M_11_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_11_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio_M_11_3.Text = string.Format("{0:F2}", strTmp[2]);
        tbxRatio_M_11_4.Text = string.Format("{0:F2}", strTmp[3]);
        tbxRatio_M_11_5.Text = string.Format("{0:F2}", strTmp[4]);
        tbxRatio_M_11_6.Text = string.Format("{0:F2}", strTmp[5]);
        tbxRatio_M_11_7.Text = string.Format("{0:F2}", strTmp[6]);
        tbxRatio_M_11_8.Text = string.Format("{0:F2}", strTmp[7]);
        tbxRatio_M_11_9.Text = string.Format("{0:F2}", strTmp[8]);
        tbxRatio_M_11_10.Text = string.Format("{0:F2}", strTmp[9]);
        tbxRatio_M_11_11.Text = string.Format("{0:F2}", strTmp[10]);
        tbxRatio_M_11_12.Text = string.Format("{0:F2}", strTmp[11]);
        tbxRatio_M_11_13.Text = string.Format("{0:F2}", strTmp[12]);
        tbxRatio_M_11_14.Text = string.Format("{0:F2}", strTmp[13]);
        tbxRatio_M_11_15.Text = string.Format("{0:F2}", strTmp[14]);
        tbxRatio_M_11_16.Text = string.Format("{0:F2}", strTmp[15]);
        tbxRatio_M_11_17.Text = string.Format("{0:F2}", strTmp[16]);
        tbxRatio_M_11_18.Text = string.Format("{0:F2}", strTmp[17]);
        tbxRatio_M_11_19.Text = string.Format("{0:F2}", strTmp[18]);
        tbxRatio_M_11_20.Text = string.Format("{0:F2}", strTmp[19]);
        tbxRatio_M_11_21.Text = string.Format("{0:F2}", strTmp[20]);
        tbxRatio_M_11_22.Text = string.Format("{0:F2}", strTmp[21]);
        tbxRatio_M_11_23.Text = string.Format("{0:F2}", strTmp[22]);
        tbxRatio_M_11_24.Text = string.Format("{0:F2}", strTmp[23]);
        tbxRatio_M_11_25.Text = string.Format("{0:F2}", strTmp[24]);
        tbxRatio_M_11_26.Text = string.Format("{0:F2}", strTmp[25]);
        tbxRatio_M_12_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_m_12"]);
        strTmp = dicConfigs["race_betratio_m_13"].Split(';');
        tbxRatio_M_13_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_13_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio_M_13_3.Text = string.Format("{0:F2}", strTmp[2]);
        tbxRatio_M_13_4.Text = string.Format("{0:F2}", strTmp[3]);
        tbxRatio_M_13_5.Text = string.Format("{0:F2}", strTmp[4]);
        strTmp = dicConfigs["race_betratio_m_14"].Split(';');
        tbxRatio_M_14_1.Text = string.Format("{0:F2}", strTmp[0]);
        tbxRatio_M_14_2.Text = string.Format("{0:F2}", strTmp[1]);
        tbxRatio_M_15_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_m_15"]);
        tbxRatio_M_16_1.Text = string.Format("{0:F2}", dicConfigs["race_betratio_m_16"]);   
    }

    protected void btnOK_Click(object sender, EventArgs e)
    {
        try
        {
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_result_mode", rblGameResult.SelectedValue });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_1", tbxRatio1_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_2", string.Format("{0};{1}", tbxRatio2_1.Text, tbxRatio2_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_3", string.Format("{0};{1}", tbxRatio3_1.Text, tbxRatio3_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_4", string.Format("{0};{1}", tbxRatio4_1.Text, tbxRatio4_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_5", string.Format("{0};{1}", tbxRatio5_1.Text, tbxRatio5_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_11",
                string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25}", 
                    tbxRatio11_1.Text, tbxRatio11_2.Text, tbxRatio11_3.Text, tbxRatio11_4.Text, tbxRatio11_5.Text, tbxRatio11_6.Text, tbxRatio11_7.Text, tbxRatio11_8.Text, tbxRatio11_9.Text, tbxRatio11_10.Text,
                    tbxRatio11_11.Text, tbxRatio11_12.Text, tbxRatio11_13.Text, tbxRatio11_14.Text, tbxRatio11_15.Text, tbxRatio11_16.Text, tbxRatio11_17.Text, tbxRatio11_18.Text, tbxRatio11_19.Text, tbxRatio11_20.Text,
                    tbxRatio11_21.Text, tbxRatio11_22.Text, tbxRatio11_23.Text, tbxRatio11_24.Text, tbxRatio11_25.Text, tbxRatio11_26.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_12", tbxRatio12_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_13", 
                string.Format("{0};{1};{2};{3};{4}", tbxRatio13_1.Text, tbxRatio13_2.Text, tbxRatio13_3.Text, tbxRatio13_4.Text, tbxRatio13_5.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_14", string.Format("{0};{1}", tbxRatio14_1.Text, tbxRatio14_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_15", tbxRatio15_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_16", tbxRatio16_1.Text });

            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_1", tbxRatio_M_1_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_2", string.Format("{0};{1}", tbxRatio_M_2_1.Text, tbxRatio_M_2_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_3", string.Format("{0};{1}", tbxRatio_M_3_1.Text, tbxRatio_M_3_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_4", string.Format("{0};{1}", tbxRatio_M_4_1.Text, tbxRatio_M_4_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_5", string.Format("{0};{1}", tbxRatio_M_5_1.Text, tbxRatio_M_5_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_11",
                string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11};{12};{13};{14};{15};{16};{17};{18};{19};{20};{21};{22};{23};{24};{25}", 
                    tbxRatio_M_11_1.Text, tbxRatio_M_11_2.Text, tbxRatio_M_11_3.Text, tbxRatio_M_11_4.Text, tbxRatio_M_11_5.Text, tbxRatio_M_11_6.Text, tbxRatio_M_11_7.Text, tbxRatio_M_11_8.Text, tbxRatio_M_11_9.Text, tbxRatio_M_11_10.Text,
                    tbxRatio_M_11_11.Text, tbxRatio_M_11_12.Text, tbxRatio_M_11_13.Text, tbxRatio_M_11_14.Text, tbxRatio_M_11_15.Text, tbxRatio_M_11_16.Text, tbxRatio_M_11_17.Text, tbxRatio_M_11_18.Text, tbxRatio_M_11_19.Text, tbxRatio_M_11_20.Text,
                    tbxRatio_M_11_21.Text, tbxRatio_M_11_22.Text, tbxRatio_M_11_23.Text, tbxRatio_M_11_24.Text, tbxRatio_M_11_25.Text, tbxRatio_M_11_26.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_12", tbxRatio_M_12_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_13", 
                string.Format("{0};{1};{2};{3};{4}", tbxRatio_M_13_1.Text, tbxRatio_M_13_2.Text, tbxRatio_M_13_3.Text, tbxRatio_M_13_4.Text, tbxRatio_M_13_5.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_14", string.Format("{0};{1}", tbxRatio_M_14_1.Text, tbxRatio_M_14_2.Text) });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_15", tbxRatio_M_15_1.Text });
            DBConn.RunStoreProcedure(Constants.SP_UPDATECONFIG, new string[] { "@cf_name", "cf_value" }, new object[] { "race_betratio_m_16", tbxRatio_M_16_1.Text });
        }
        catch (Exception ex)
        {
            ShowMessageBox(Resources.Err.ERR_DBERROR + "\\n" + ex.Message);
            return;
        }

        Response.Redirect("/Config/RaceConfig.aspx");
    }
}
