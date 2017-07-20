using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapperView
{
    class SpatialiteReader
    {
        #region Notes
        #endregion
        #region Fields
        private DataBase_Reader.SQLiteReader _SqliteReader;
        #endregion
        #region Properties
        #endregion
        #region Constructors
        #endregion
        #region Voids
        #endregion
        #region Functions
        public T ToFeatures<T>(string TableName, string GeometryColumnName) where T : LifeSimGIS.Features
        {
            _SqliteReader.SetTableReader(TableName);
            int index = Array.IndexOf(_SqliteReader.ColumnNames, GeometryColumnName);
            if (index == -1) { throw new Exception("Column Name " + GeometryColumnName + " was not found in table " + TableName + "."); }
            return ToFeatures<T>(TableName, index);
            //_SqliteReader.
            //return null;
        }
        public T ToFeatures<T>(string TableName, int GeometryColumnIndex) where T : LifeSimGIS.Features
        {
            //http://www.gaia-gis.it/gaia-sins/BLOB-Geometry.html
            _SqliteReader.SetTableReader(TableName);
            //_SqliteReader.
            object[] rowdata = _SqliteReader.GetColumn(GeometryColumnIndex);
            int rowcount = rowdata.Count();
            for (int i = 0; i < rowcount; i++){
                
                byte[] bytes = (byte[])rowdata[i];
                //check to see if byte 38 can exist?
                if(bytes.Length<38) { throw new Exception("Row " + i + " did not contain sufficent length to provide byte 38 to determine if this is a Geometry BLOB."); }
                //check byte 38 is 0x7C
                if (bytes[38] != 0x7c) { throw new Exception("Row " + i + " did not properly encode byte 38 to 0x7C indicating a Geometry BLOB."); }
                //check T matches with bytes 39-42
                Int32 geomtype = BitConverter.ToInt32(bytes, 39);

            }
            
            return null;
        }
        #endregion
    }
}
