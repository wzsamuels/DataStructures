using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Data
{
    /**
     * An identifiable object that has an ID number (integer)
    * 
    * @author Dr. King
    * @author Zach Samuels
    */
    public interface IIdentifiable
    {
        /**
        * Getter for an object's id field.
        * 
        * @return The objects id as an integer.
        */
        int GetId();
    }
}
