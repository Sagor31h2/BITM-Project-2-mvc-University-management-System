using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using Microsoft.Owin.Security.Facebook;
using UniversityCourseManagementSystem.Gateway;
using UniversityCourseManagementSystem.Models;
using UniversityCourseManagementSystem.Models.ViewModels;

namespace UniversityCourseManagementSystem.Manager
{
   
    public class AllocatedClassroomManager
    {
        AllocatedClassroomGateway allocatedClassroomGateway = new AllocatedClassroomGateway();

        public string Save(AllocatedClassroom aClassroom)
        {
            aClassroom.Status = true;
            if (allocatedClassroomGateway.CheckRoomAndDay(aClassroom) && allocatedClassroomGateway.CheckTimeSchedule(aClassroom))
            {
               
                    return "This room is already allocated"; 
            }
            else
            {
                int rowAffected = allocatedClassroomGateway.Save(aClassroom);
                if (rowAffected > 0)
                {
                    return "Room allocated successfully";
                }
                return "Allocating failed";
            }
        }
        public List<AllocatedClassroom> GetAllRooms()
        {
            return allocatedClassroomGateway.GetAllRooms();
        }

        public List<string> GetDays()
        {
            return allocatedClassroomGateway.GetAllDays();
        }

        public List<ViewScheduleInfo> GetAllInfo()
        {
            return allocatedClassroomGateway.GetAllInfo();
        }
    }
}