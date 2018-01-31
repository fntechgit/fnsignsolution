using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;

namespace schedInterface
{
    public class imports
    {
        private schedDataContext db = new schedDataContext();
        private sessions _sessions = new sessions();
        private locations _locations = new locations();

        public ImportHistory create(ImportHistory i)
        {
            import_history entity = new import_history();
            entity.event_id = i.event_id;
            entity.comments = i.comments;
            entity.createdate = DateTime.Now;
            entity.errored = i.errored;
            entity.imported = i.imported;
            entity.imported_by = i.imported_by;
            entity.status = i.status;
            entity.total = i.total;
            entity.type = i.type;
            entity.filename = i.filename;
            this.db.import_histories.InsertOnSubmit(entity);
            this.db.SubmitChanges();
            i.id = entity.id;
            return i;
        }

        public List<ImportHistory> all()
        {
            List<ImportHistory> importHistoryList = new List<ImportHistory>();
            Table<import_history> importHistories = this.db.import_histories;
            Expression<Func<import_history, int>> keySelector = (Expression<Func<import_history, int>>)(his => his.id);
            foreach (import_history importHistory in (IEnumerable<import_history>)importHistories.OrderByDescending<import_history, int>(keySelector))
                importHistoryList.Add(new ImportHistory()
                {
                    event_id = importHistory.event_id,
                    comments = importHistory.comments,
                    errored = importHistory.errored,
                    id = importHistory.id,
                    imported = importHistory.imported,
                    imported_by = importHistory.imported_by,
                    status = importHistory.status,
                    total = importHistory.total,
                    type = importHistory.type,
                    createdate = importHistory.createdate,
                    filename = importHistory.filename
                });
            return importHistoryList;
        }

        public List<ImportHistory> by_event(int id)
        {
            return this.all().Where<ImportHistory>((Func<ImportHistory, bool>)(x => x.event_id == id)).ToList<ImportHistory>();
        }

        public ImportHistory single(int id)
        {
            return this.all().Single<ImportHistory>((Func<ImportHistory, bool>)(x => x.id == id));
        }

        public ImportHistory parseData(DataTable dt, int event_id)
        {
            ImportHistory importHistory = new ImportHistory();
            importHistory.event_id = event_id;
            importHistory.errored = 0;
            importHistory.imported = 0;
            importHistory.total = 0;
            foreach (DataRow row in (InternalDataCollectionBase)dt.Rows)
            {
                Session s = new Session();
                s.name = row[0].ToString();
                DateTime result1;
                if (DateTime.TryParse(row[1].ToString() + " " + row[2].ToString(), out result1))
                    s.start = result1;
                DateTime result2;
                if (DateTime.TryParse(row[3].ToString() + " " + row[4].ToString(), out result2))
                    s.end = result2;
                s.event_type = row[5].ToString();
                s.venue = row[6].ToString();
                s.event_id = event_id;
                s.venue_id = this._locations.find_venue_reference_id(s.venue, s.event_id);
                s.event_key = "IMPORT_" + Guid.NewGuid().ToString();
                s.speakers = row[7].ToString();
                s.speaker_companies = row[8].ToString();
                s.speaker_images = row[9].ToString();
                if (this.insert(s))
                    ++importHistory.imported;
                else
                    ++importHistory.errored;
            }
            return importHistory;
        }

        public bool insert(Session s)
        {
            try
            {
                this.db.sessions_import(s.name, new DateTime?(s.start), new DateTime?(s.end), s.event_type, s.venue_id, s.venue, new int?(s.event_id), s.event_key, s.speakers, s.speaker_companies, s.speaker_images);
                return true;
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
    }

    public class ImportHistory
    {
        public int id { get; set; }

        public int event_id { get; set; }

        public DateTime createdate { get; set; }

        public string status { get; set; }

        public int imported { get; set; }

        public int errored { get; set; }

        public int total { get; set; }

        public string comments { get; set; }

        public string type { get; set; }

        public string imported_by { get; set; }

        public string filename { get; set; }
    }
}
