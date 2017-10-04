using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Clinic_CheckRoom
{
    public class CheckRoomFilterList_Class
    {
        /// <summary>
        /// 1.3.获取限制列表
        /// </summary>
        /// <returns></returns>
        public static DataTable GetRoomFitterList(string p_dep, string p_modality, string p_checkroomno, DateTime p_checkdate)
        {
            DataTable dt = new DataTable();
            string str = "select * from checkroomlist c,checkroomfilterlist d,check_room e";
            str += "(select a.checkroomno, a.listno, a.checkdate, count(a.id) counts from appointmentlist a";
            str += " where a.check_status = '未预约' and a.checkdate = to_date('" + p_checkdate.ToString("yyyy-MM-dd") + "', 'yyyy-MM-dd')";
            str += "group by a.checkroomno, a.listno, a.checkdate) b";
            str += " where c.checkroomno = b.checkroomno and c.listno = b.listno and c.checkroomno = e.checkroomno ";
            if (p_dep != "")
            {
                str += " and  e.dep ='" + p_dep + "' ";
            }
            if (p_modality != "")
            {
                str += " and  e.modality ='" + p_modality + "' ";
            }
            if (p_checkroomno != "")
            {
                str += " and  c.checkroomno ='" + p_checkroomno + "' ";
            }
            str += "and c.checkroomno = d.checkroomno(+) and c.listno = d.listno(+)";
            str += "order by c.checkdate,c.begintime, c.checkroomno , c.listno,d.ptype ";
            return dt;
        }


        public static bool checkroomfilter(DataTable dt, string ptype, string p_pattype, string p_sex, string p_checktype, string p_checkpos, string p_hbsag)
        {
            //  flog_Class.WriteFlog("限制条件：check_room ='" + p_checkroom + "'and ptype ='" + ptype + "' 的数量：" + dt.Rows.Count .ToString) '将详细错误信息写入日志

            var selectstatus = true;
            var str = "";
            str = ptype == "0" ? "阻止" : "允许";

            foreach (DataRow item in dt.Rows)
            {
                selectstatus = true;

                if (tjpd(item["pattype"].ToString(), p_pattype) == false)
                {
                    selectstatus = false;
                    //flog_Class.WriteFlog(str + "条件：患者类型：" + p_class.pattype + "；实际： " + p_pattype.ToString)
                    continue;
                }

                if (tjpd(item["sex"].ToString(), p_sex) == false)
                {
                    selectstatus = false;
                    //  flog_Class.WriteFlog(str + "限制条件：患者性别：" + p_class.sex + "；实际： " + p_sex.ToString)
                    continue;
                }
                if (tjpd(item["HBSAG"].ToString(), p_hbsag) == false)
                {
                    selectstatus = false;
                    // flog_Class.WriteFlog(str + "限制条件：HBSAG：" + p_class.HBSAG + "；实际： " + p_hbsag.ToString)
                    continue;
                }
                if (tjpd(item["checktype"].ToString(), p_checktype) == false)
                {
                    selectstatus = false;
                    //  flog_Class.WriteFlog(str + "限制条件：检查类型：" + p_class.checktype + "；实际： " + p_checktype.ToString)
                    continue;
                }
                if (item["checkpos"].ToString() != "")
                {
                    string[] temp = p_checkpos.Split(new char[] { '、' });
                    foreach (string itemtemp in temp)
                    {
                        if (tjpd(item["checkpos"].ToString(), itemtemp) == false)
                        {
                            selectstatus = false;
                            continue;
                        }
                    }
                }
            }
            return selectstatus;
        }
        private static bool tjpd(string p_item, string p_newitem)
        {
            if (p_item == "")
            {
                return true;
            }
            else if (p_item == p_newitem)
            {
                return true;
            }
            else
            {
                return false;

            }
        }

    }
}
