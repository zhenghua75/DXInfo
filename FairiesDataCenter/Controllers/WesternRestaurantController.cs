using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ynhnTransportManage.Models;
using Trirand.Web.Mvc;
using DXInfo.Data.Contracts;
using Ninject;
using System.Data.Objects.SqlClient;

namespace ynhnTransportManage.Controllers
{
    /// <summary>
    /// 西餐厅
    /// </summary>
    public class WesternRestaurantController : BaseController
    {
        #region 构造
        public WesternRestaurantController(IFairiesMemberManageUow uow):base(uow)
        {
            this.Uow.Db.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ COMMITTED;");    
        }
        #endregion

        #region Index
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        #endregion

        #region 房间
        private void SetupRoomGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("Room_RequestData");
            grid.EditUrl = Url.Action("Room_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "DeptId", this.GetDept());
            SetDropDownColumn(grid, "Status", centerCommon.GetRoomStatus());
        }
        [Authorize]
        public ActionResult Room()
        {
            var gridModel = new RoomGridModel();
            SetupRoomGridModel(gridModel.RoomGrid);
            return View(gridModel);
        }
        public ActionResult Room_RequestData()
        {
            var gridModel = new RoomGridModel();
            SetupRoomGridModel(gridModel.RoomGrid);

            var q = from r in Uow.Rooms.GetAll()
                      join d in Uow.Depts.GetAll() on r.DeptId equals d.DeptId into rd
                      from rds in rd.DefaultIfEmpty()
                      join d1 in Uow.NameCode.GetAll().Where(w=>w.Type=="RoomStatus") on SqlFunctions.StringConvert((double?)r.Status).Trim() equals d1.Code into dd1
                      from dd1s in dd1.DefaultIfEmpty()
                      select new { r.Id, r.DeptId, r.Code, r.Name, r.Comment, r.Status,StatusName=dd1s.Name, rds.DeptName };
            return QueryAndExcel(gridModel.RoomGrid, q, "房间.xls");
        }
        private void addRoom(DXInfo.Models.Rooms room)
        {
            Uow.Rooms.Add(room);
            Uow.Commit();
        }
        private void editRoom(DXInfo.Models.Rooms room)
        {
            var oldRoom = Uow.Rooms.GetById(g => g.Id == room.Id);
            oldRoom.Comment = room.Comment;
            oldRoom.DeptId = room.DeptId;
            oldRoom.Code = room.Code;
            oldRoom.Name = room.Name;
            oldRoom.Status = room.Status;
            Uow.Rooms.Update(oldRoom);
            Uow.Commit();
        }
        private void delRoom(DXInfo.Models.Rooms room)
        {
            var oldRoom = Uow.Rooms.GetById(g => g.Id == room.Id);
            if (oldRoom != null)
            {
                Uow.Rooms.Delete(oldRoom);
                Uow.Commit();
            }
        }

        public ActionResult Room_EditData(DXInfo.Models.Rooms room)
        {
            var gridModel = new RoomGridModel();
            SetupRoomGridModel(gridModel.RoomGrid);
            return ajaxCallBack<DXInfo.Models.Rooms>(gridModel.RoomGrid, room, addRoom, editRoom, delRoom);
        }
        #endregion

        #region 桌台
        private void SetupDeskGridModel(DeskGridModel gridModel)
        {
            var grid = gridModel.DeskGrid;
            grid.DataUrl = Url.Action("Desk_RequestData");
            grid.EditUrl = Url.Action("Desk_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "RoomId", centerCommon.GetRoom());
            SetDropDownColumn(grid, "Status", centerCommon.GetDeskStatus());
        }
        [Authorize]
        public ActionResult Desk()
        {
            var gridModel = new DeskGridModel();            
            SetupDeskGridModel(gridModel);
            return View(gridModel);
        }
        public ActionResult Desk_RequestData()
        {
            var gridModel = new DeskGridModel();
            SetupDeskGridModel(gridModel);
            var q = from d in Uow.Desks.GetAll()
                    join d1 in Uow.Rooms.GetAll() on d.RoomId equals d1.Id into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    join d2 in Uow.NameCode.GetAll().Where(w => w.Type == "DeskStatus") on SqlFunctions.StringConvert((double?)d.Status).Trim() equals d2.Code into dd2
                    from dd2s in dd2.DefaultIfEmpty()
                    select new
                    {
                        d.Id,
                        d.RoomId,
                        RoomName=dd1s.Name,
                        d.Code,
                        d.Name,
                        d.Size,
                        d.Comment,
                        d.Status,
                        StatusName=dd2s.Name,
                    };
            return QueryAndExcel(gridModel.DeskGrid, q, "桌台.xls");
        }
        private void addDesk(DXInfo.Models.Desks desk)
        {
            var d = Uow.Desks.GetAll().Where(w => w.Code == desk.Code);
            if (d.Count() > 0)
            {
                throw new DXInfo.Models.BusinessException("桌号不能重复");
            }
            Uow.Desks.Add(desk);
            Uow.Commit();
        }
        private void editDesk(DXInfo.Models.Desks desk)
        {
            var oldDesk = Uow.Desks.GetById(g => g.Id == desk.Id);
            oldDesk.Comment = desk.Comment;
            oldDesk.Code = desk.Code;
            oldDesk.Name = desk.Name;
            oldDesk.Status = desk.Status;
            oldDesk.Size = desk.Size;
            Uow.Desks.Update(oldDesk);
            Uow.Commit();
        }
        private void delDesk(DXInfo.Models.Desks desk)
        {
            var oldDesk = Uow.Desks.GetById(g => g.Id == desk.Id);
            if (oldDesk != null)
            {
                Uow.Desks.Delete(oldDesk);
                Uow.Commit();
            }
        }

        public ActionResult Desk_EditData(DXInfo.Models.Desks desk)
        {
            var gridModel = new DeskGridModel();
            SetupDeskGridModel(gridModel);
            return ajaxCallBack<DXInfo.Models.Desks>(gridModel.DeskGrid, desk, addDesk, editDesk, delDesk);
        }
        
        #endregion

        #region IPad
        private void SetupIPadGridModel(JQGrid grid)
        {
            grid.DataUrl = Url.Action("IPad_RequestData");
            grid.EditUrl = Url.Action("IPad_EditData");
            SetUpGrid(grid);
            SetDropDownColumn(grid, "Status", centerCommon.GetIPadStatus());
        }
        [Authorize]
        public ActionResult IPad()
        {
            var gridModel = new IPadGridModel();
            SetupIPadGridModel(gridModel.IPadGrid);
            return View(gridModel);
        }
        public ActionResult IPad_RequestData()
        {
            var gridModel = new IPadGridModel();
            SetupIPadGridModel(gridModel.IPadGrid);

            var q = from r in Uow.IPads.GetAll()
                    join d1 in Uow.NameCode.GetAll().Where(w => w.Type == "IPadStatus") on SqlFunctions.StringConvert((double?)r.Status).Trim() equals d1.Code into dd1
                    from dd1s in dd1.DefaultIfEmpty()
                    select new
                    {
                        r.Id,
                        r.Code,
                        r.Name,
                        r.Status,
                        r.Comment,
                        r.SN,
                        StatusName=dd1s.Name,
                    };
            return QueryAndExcel(gridModel.IPadGrid, q, "IPad.xls");
        }
        private void addIPad(DXInfo.Models.IPads iPad)
        {
            Uow.IPads.Add(iPad);
            Uow.Commit();
        }
        private void editIPad(DXInfo.Models.IPads iPad)
        {
            var oldIPad = Uow.IPads.GetById(g => g.Id == iPad.Id);
            oldIPad.Comment = iPad.Comment;
            oldIPad.SN = iPad.SN;
            oldIPad.Code = iPad.Code;
            oldIPad.Name = iPad.Name;
            oldIPad.Status = iPad.Status;
            Uow.IPads.Update(oldIPad);
            Uow.Commit();
        }
        private void delIPad(DXInfo.Models.IPads iPad)
        {
            var oldIPad = Uow.IPads.GetById(g => g.Id == iPad.Id);
            if (oldIPad != null)
            {
                Uow.IPads.Delete(oldIPad);
                Uow.Commit();
            }
        }

        public ActionResult IPad_EditData(DXInfo.Models.IPads iPad)
        {
            var gridModel = new IPadGridModel();
            SetupIPadGridModel(gridModel.IPadGrid);
            return ajaxCallBack<DXInfo.Models.IPads>(gridModel.IPadGrid, iPad, addIPad, editIPad, delIPad);
        }
        #endregion
    }
}
