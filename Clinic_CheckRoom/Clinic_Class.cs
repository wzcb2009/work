using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Clinic_CheckRoom
{
    public class Clinic_Class
    {
        #region 自动预约步骤
        /// <summary>
        /// 1.1.获取机房列表
        /// </summary>
        /// <param name="p_dep"></param>
        /// <param name="p_modality"></param>
        /// <returns></returns>
        public DataTable GetRoomByModality(string p_dep, string p_modality)
        {
            DataTable dt = new DataTable();
            dt = Check_Room_Class.GetRoomByModality(p_dep, p_modality);
            return dt;
        }
        /// <summary>
        /// 1.2.检测当天的预约列表是否存在
        /// </summary>
        /// <param name="p_checkroomno"></param>
        /// <param name="p_checkdate"></param>
        public void CheckAppointmentList(string p_checkroomno, DateTime p_checkdate)
        {
            DataTable dt = AppointmentList_Class.GetAppointmentList(p_checkroomno, p_checkdate);
            if ((dt == null) || (dt.Rows.Count == 0))
            {

            }
        }
        /// <summary>
        /// 1.3.获取限制列表
        /// </summary>
        /// <returns></returns>
        public DataTable GetRoomFitterList(string p_dep, string p_modality, DateTime p_checkdate)
        {
            DataTable dt = new DataTable();
            dt = CheckRoomFilterList_Class.GetRoomFitterList(p_dep, p_modality, "", p_checkdate);
            return dt;
        }
        /// <summary>
        /// 1.4.检测限制列表是否匹配
        /// </summary>
        /// <returns></returns>
        public string CheckRoomFitterList(DataTable dt)
        {
            var pdt = dt.Clone();
            int oldtype = -1;
            string oldlistno = "";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string listno = dt.Rows[i]["listno"].ToString();
                DateTime checkdate = Convert.ToDateTime(dt.Rows[i]["checkdate"]);
                if (listno != oldlistno)
                {
                    if (oldlistno != "")
                    { //检查规则

                    }
                    pdt.Rows.Clear();
                    oldtype = -1;
                    listno = oldlistno;
                }
                if (listno == "")
                {
                    return GetAppointmentKey(listno, checkdate);
                }
                else
                {
                    int newtype = Convert.ToInt32(dt.Rows[i]["ptype"]);
                    if (newtype != oldtype)
                    {
                        if (oldtype != -1)
                        { //检查规则

                        }
                        pdt.Rows.Clear();
                        newtype = oldtype;
                    }
                    pdt.Rows.Add(dt.Rows[i].ItemArray);
                }
            }
            if (pdt.Rows.Count > 0)
            {
                //检查规则
            }
            return "";
        }
        public string GetAppointmentKey(string p_listno, DateTime p_checkdate)
        {
            var list = AppointmentList_Class.GetAppointmentKey(p_listno, p_checkdate, "未预约", p_checkdate.Date, "", "");
            return list.ID.ToString();
        }

        public string GetAutoCallNo(string p_dep, string p_modality)
        {
            DataTable dt = GetRoomByModality(p_dep, p_modality);
            DateTime p_checkdate = DateTime.Now.Date;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string checkroomno = dt.Rows[i]["checkroomno"].ToString();
                CheckAppointmentList(checkroomno, p_checkdate);
            }
            dt = GetRoomFitterList(p_dep, p_modality, p_checkdate);

            return "";
        }
        #endregion

        #region 更新预约列表
        //获取分时段数据
        public DataTable GetRoomList(string p_checkroomno, DateTime p_checkdate)
        {
            DataTable dt = new DataTable();

            return dt;
        }
        //获取分时段数据失败，读取上一周的数据
        //获取分时段数据失败，读取上一周的数据
        //插入当天分时段数据
        private void InsertCheckRoomList(DataTable dt)
        {

        }
        //生成分时段图形列表  
        private void CreateAppointmentList()
        {

        }
        #endregion

        #region 网络手动预约
        //自动预约前4步
        //显示七天内所有匹配数据的机房数量
        //等待用户选择机房和时间
        //显示当天本机房的预约图形列表
        //打印号码单  
        #endregion
    }
}
