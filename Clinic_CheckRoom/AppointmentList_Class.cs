using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Clinic_CheckRoom
{

    public class AppointmentList_Class
    {
        public int ID { get; set; }
        public string checkroomno { get; set; }
        public string listno { get; set; }
        public DateTime checkdate { get; set; }
        public DateTime begintime { get; set; }
        public DateTime endtime { get; set; }
        public string remark { get; set; }
        public string remark2 { get; set; }
        public string accessno { get; set; }
        public string callno { get; set; }
        public string check_status { get; set; }
        public string timestring { get; set; }

        public AppointmentList_Class()
        { }
        public AppointmentList_Class(int p_id)
        {
            DataTable dt = new DataTable();
            string str = "select * from appointmentlist where id ='" + p_id + "' ";
            dt = sql_Class.GetDataTable(str);
            if (dt == null)
                return;
            if (dt.Rows.Count == 0)
                return;
            FillClass(dt.Rows[0]);
        }
        public AppointmentList_Class(DataRow dw)
        {
            FillClass(dw);
        }
        private void FillClass(DataRow dw)
        {
            ID = Convert.ToInt32(dw["ID"]);
            checkroomno = dw["checkroomno"].ToString();
            listno = dw["listno"].ToString();
            checkdate = Convert.ToDateTime(dw["checkdate"]);
            begintime = Convert.ToDateTime(dw["begintime"]);
            endtime = Convert.ToDateTime(dw["endtime"]);
            remark = dw["remark"].ToString();
            remark2 = dw["remark2"].ToString();
            accessno = dw["accessno"].ToString();
            callno = dw["callno"].ToString();
            check_status = dw["check_status"].ToString();
            timestring = dw["timestring"].ToString();
        }
        public static DataTable GetAppointmentList(string p_checkroomno, DateTime p_checkdate)
        {
            DataTable dt = new DataTable();
            string str = "select * from appointmentlist where checkroomno='" + p_checkroomno + "' and checkdate = to_date('" + p_checkdate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd') ";
            dt = sql_Class.GetDataTable(str);
            return dt;
        }
        public static AppointmentList_Class GetAppointmentKey(string p_listno, DateTime p_checkdate, string check_status, DateTime begintime, string checkroomno, string accessno)
        {
            DataTable dt = new DataTable();
            var classlist = new AppointmentList_Class();
            string str = "select * from appointmentlist where listno='" + p_listno + "' and checkdate = to_date('" + p_checkdate.ToString("yyyy-MM-dd") + "','yyyy-MM-dd') ";
            if (check_status != "")
            {
                str += " and  check_status ='" + check_status + "' ";
            }
            if (checkroomno != "")
            {
                str += " and  checkroomno ='" + checkroomno + "' ";
            }
            if (accessno != "")
            {
                str += " and  accessno ='" + accessno + "' ";
            }
            if (begintime != null && (begintime.Year != 1900))
            {
                str += " and  begintime = to_date('" + begintime.ToString("yyyy-MM-dd hh:mm:ss") + "','yyyy-MM-dd hh24:mi:ss')";
            }
             
            str += " order by begintime ";

            dt = sql_Class.GetDataTable(str);
            dt = sql_Class.GetDataTable(str);
            if (dt == null)
                return classlist;
            if (dt.Rows.Count == 0)
                return classlist;
            classlist = new AppointmentList_Class(dt.Rows[0]);
            return classlist;
        }

    }
}
