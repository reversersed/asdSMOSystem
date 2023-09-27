public class ListNode
{
    public Client data;
    public ListNode next;

    public ListNode(Client data)
    {
        this.data = data;
        this.next = null;
    }
}
public class Queue
{
    ListNode front;
    ListNode current;

    public Queue()
    {
        front = null;
        current = null;
    }

    public bool Enqueue(Client data)
    {
        ListNode node = new ListNode(data);
        if(front == null)
        {
            front = current = node;
            return true;
        }
        current.next = node;
        current = node;

        return true;
    }
    public bool Pop(ref Client Node)
    {
        if (front == null)
            return false;

        Node = front.data;
        front = front.next;

        return true;
    }
    public bool Peek(ref Client Node)
    {
        if (front == null)
            return false;

        Node = front.data;

        return true;
    }
    public bool IsEmpty() { return front == null; }
}
