public class Cashier
{
    public enum State
    {
        Free = 0,
        Busy = 1
    }
    public Client currentClient { get; private set; }
    public State state {  get; private set; }

    public Cashier()
    {
        currentClient = null;
        state = State.Free;
    }
    public bool SetClient(Client client)
    {
        if(state == State.Busy)
            return false;
        currentClient = client;
        state = State.Busy;
        return true;
    }
    public bool FreeClient(ref Client client)
    {
        if(state == State.Free)
            return false;
        client = currentClient;
        currentClient = null;
        state = State.Free;
        return true;
    }
}
