using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Client
{
    public int ArriveTime { get; set; }
    public int LeaveTime { get; set; }

    public Client(int arrivetime)
    {
        ArriveTime = arrivetime;
        LeaveTime = -1;
    }
    public Client()
    {
        ArriveTime = 0;
        LeaveTime = 0;
    }
}
