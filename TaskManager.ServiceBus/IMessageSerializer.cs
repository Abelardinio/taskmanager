namespace TaskManager.ServiceBus
{
    public interface IMessageSerializer
    {
        byte[] Serialize(object obj);

        object Deserialize(byte[] input);
    }
}