using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HRApplication.Models
{
    public class PositionContext : DbContext
    {
        public PositionContext(DbContextOptions<PositionContext> options)
            : base(options)
        {
        }

        public DbSet<Position> Position { get; set; }
        public DbSet<Application> Application { get; set; }

        public List<Position> getPositionsByCriteria(string column, string criteria)
        {
            string searchColumn = column.ToLower();
            string searchCriteria = criteria.ToLower();
            List<Position> res = new List<Position>();

            foreach (Position position in Position.ToList())
            {
                switch (searchColumn)
                {
                    case "id":
                        var pos = Position.Find(criteria);
                        if (pos != null)
                        {
                            res.Add(pos);
                        }
                        break;
                    case "location":
                        if (position.Location.ToLower().Contains(searchCriteria))
                        {
                            res.Add(position);
                        }
                        break;
                    case "title":
                        if (position.Title.ToLower().Contains(searchCriteria))
                        {
                            res.Add(position);
                        }
                        break;
                    case "description":
                        if (position.Description.ToLower().Contains(searchCriteria))
                        {
                            res.Add(position);
                        }
                        break;
                    default:
                        break;
                }
            }
            
            return res;
        }

        public List<Application> GetApplicationsForUser(string userId)
        {
            List<Application> res = new List<Application>();

            foreach(Application application in Application.ToList())
            {
                if (application.ApplicantID.Equals(userId))
                {
                    res.Add(application);
                }
            }

            return res;
        }

        public List<Application> GetApplicationsForPosition(string positionID)
        {
            List<Application> res = new List<Application>();

            foreach (Application application in Application.ToList())
            {
                if (application.PositionID.Equals(positionID))
                {
                    res.Add(application);
                }
            }

            return res;
        }

        public List<Position> GetPositionsForApplications(List<Application> applications)
        {
            List<Position> res = new List<Position>();
            foreach(Application application in applications)
            {
                res.Add(Position.SingleOrDefault(m => m.ID == application.PositionID));
            }
            return res;
        }
    }
}
