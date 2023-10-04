public class CalendarNode
{
    public int id;
    public int time;
    public CalendarNode? next;

    public CalendarNode(int id, int time)
    {
        this.id = id;
        this.time = time;
        next = null;
    }
}
public class Calendar
{
    CalendarNode? first;
    public int Size { get; private set; }

    public Calendar()
    {
        first = null;
        Size = 0;
    }

    public bool Add(int id, int time)
    {
        CalendarNode node = new CalendarNode(id,time);
        if (first == null)
        {
            first = node;
            Size = 1;
            return true;
        }
        if(first.time >= node.time)
        {
            CalendarNode temp = first;
            first = node;
            first.next = temp;
            Size += 1;
            return true;
        }
        if (!this.Add(ref first.next, node))
            return false;
        Size += 1;
        return true;
    }
    private bool Add(ref CalendarNode? start, CalendarNode node)
    {
        if (start == null || start.time >= node.time)
        {
            CalendarNode? temp = start;
            start = node;
            start.next = temp;
            return true;
        }
        else if (this.Add(ref start.next, node))
            return true;
        return false;
    }
    public bool Pop(ref int id, ref int time)
    {
        if (first == null)
            return false;

        id = first.id;
        time = first.time;
        first = first.next;

        Size -= 1;
        return true;
    }
    public bool Peek(ref int id, ref int time)
    {
        if (first == null)
            return false;

        id = first.id;
        time = first.time;

        return true;
    }
}