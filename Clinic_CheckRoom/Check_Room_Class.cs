using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_CheckRoom
{
    public class Check_Room_Class
    {


        public static DataTable GetRoomByModality(string p_dep, string p_modality)
        {
            DataTable dt = new DataTable();
            string str = "select * from check_room where dep='" + p_dep + "' and modality='" + p_modality + "' and  Check_Room_State='正常'";
            dt = sql_Class.GetDataTable(str);
            return dt;
        }
    }
}
