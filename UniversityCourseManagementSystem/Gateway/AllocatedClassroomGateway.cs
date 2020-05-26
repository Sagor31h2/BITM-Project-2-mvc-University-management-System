using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Gateway
{
    public class AllocatedClassroomGateway : BaseGateway
    {
        public List<AllocatedClassroom> GetAllRooms()
        {
            string query = "SELECT * FROM Room";
            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<AllocatedClassroom> allRooms = new List<AllocatedClassroom>();
            while (Reader.Read())
            {
                AllocatedClassroom aRoom = new AllocatedClassroom();
                aRoom.RoomId = (int)Reader["Id"];
                aRoom.RoomNo = Reader["RoomNo"].ToString();
                allRooms.Add(aRoom);
            }

            Reader.Close();
            Connection.Close();
            return allRooms;
        }

        public List<string> GetAllDays()
        {
            List<string> days = new List<string>()
            {
                "Saturday","Sunday","Monday","Tuesday","Wednesday","Thursday","Friday"
            };
            return days;
        }
        public int Save(AllocatedClassroom aClassroom)
        {

            string query = "INSERT INTO AllocatedRooms VALUES(@departmentId,@courseId,@roomId,@day,@startTime,@finishTime,@status)";

            Command = new SqlCommand(query, Connection);

            Command.Parameters.Clear();
            Command.Parameters.Add("departmentId", SqlDbType.Int);
            Command.Parameters["departmentId"].Value = aClassroom.DepartmentId;

            Command.Parameters.Add("courseId", SqlDbType.Int);
            Command.Parameters["courseId"].Value = aClassroom.CourseId;

            Command.Parameters.Add("roomId", SqlDbType.Int);
            Command.Parameters["roomId"].Value = aClassroom.RoomId;

            Command.Parameters.Add("day", SqlDbType.VarChar);
            Command.Parameters["day"].Value = aClassroom.Day;

            Command.Parameters.Add("startTime", SqlDbType.VarChar);
            Command.Parameters["startTime"].Value = aClassroom.FromTime;

            Command.Parameters.Add("finishTime", SqlDbType.VarChar);
            Command.Parameters["finishTime"].Value = aClassroom.ToTime;

            Command.Parameters.Add("status", SqlDbType.Bit);
            Command.Parameters["status"].Value = aClassroom.Status;

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public bool CheckRoomAndDay(AllocatedClassroom aClassroom)
        {
            string query = "SELECT * FROM AllocatedRooms WHERE Day = '" + aClassroom.Day + "' AND RoomId = '"+ aClassroom.RoomId +"' ";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            bool hasRow = false;

            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;
        }
        public bool CheckTimeSchedule(AllocatedClassroom aClassroom)
        {
            string query = "SELECT * FROM AllocatedRooms WHERE (StartTime >= '"+ aClassroom.FromTime +"' AND FinishTime <= '"+ aClassroom.ToTime +"') OR (StartTime <= '"+ aClassroom.ToTime  +"' AND " +
                           " FinishTime >= '"+aClassroom.FromTime+"')";

            Command = new SqlCommand(query, Connection);

            Connection.Open();

            Reader = Command.ExecuteReader();

            bool hasRow = false;

            if (Reader.HasRows)
            {
                hasRow = true;
            }

            Reader.Close();
            Connection.Close();

            return hasRow;
        }
        public List<ViewScheduleInfo> GetAllInfo()
        {
            string query = "SELECT * FROM GetScheduleInfo";

            Command = new SqlCommand(query, Connection);
            Connection.Open();
            Reader = Command.ExecuteReader();

            List<ViewScheduleInfo> allInfo = new List<ViewScheduleInfo>();

            while (Reader.Read())
            {
                ViewScheduleInfo scheduleInfo = new ViewScheduleInfo();

                scheduleInfo.DepartmentId = (int) Reader["Id"];
                scheduleInfo.CourseCode = Reader["CourseCode"].ToString();
                scheduleInfo.CourseName = Reader["CourseName"].ToString();
                scheduleInfo.ScheduleInfo = Reader["Schedule Info"].ToString();

                if (scheduleInfo.ScheduleInfo == "")
                {
                    scheduleInfo.ScheduleInfo = "Not Schedule Yet";
                }
                allInfo.Add(scheduleInfo);
            }
            Reader.Close();
            Connection.Close();

            return allInfo;
        }
    }
}