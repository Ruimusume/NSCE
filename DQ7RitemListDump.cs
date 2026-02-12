using static System.Buffers.Binary.BinaryPrimitives;

namespace DQ7RTools;

public partial class ListItems
{
    // 初始化参数
    public static int Count = 506;
    public static int Size = Count * ListItem.Size;

    public byte[] Data;

    public List<ListItem> GetList()
    {
        List<ListItem> ItemList = new();

        // 原始数据
        byte[] originalData = this.Data;

        // 每个分块大小
        int chunkSize = ListItem.Size;


        // 计算分块数量
        int totalChunks = (originalData.Length + chunkSize - 1) / chunkSize;

        for (int i = 0; i < totalChunks; i++)
        {
            // 计算当前分块大小（最后一块可能不足100字节）
            int currentChunkSize = Math.Min(chunkSize, originalData.Length - i * chunkSize);

            byte[] chunk = new byte[currentChunkSize];

            Array.Copy(originalData, i * chunkSize, chunk, 0, currentChunkSize);

            ListItem item = new ListItem(chunk);

            ItemList.Add(item);
        }

        return ItemList;
    }


    public ListItems(byte[] Data = default)
    {
        this.Data = Data == default ? new byte[Size] : Data;
    }
}

public partial class ListItem
{

    // 初始化参数
    public static int Size = 0x18;
    public byte[] Data;

    public uint ItemId { get => ReadUInt32LittleEndian(Data.AsSpan(0x0)); set => WriteUInt32LittleEndian(Data.AsSpan(0x0), value); }
    public uint Unkown1 { get => ReadUInt32LittleEndian(Data.AsSpan(0x4)); set => WriteUInt32LittleEndian(Data.AsSpan(0x4), value); }
    public uint Unkown2 { get => ReadUInt32LittleEndian(Data.AsSpan(0x8)); set => WriteUInt32LittleEndian(Data.AsSpan(0x8), value); }
    public uint Unkown3 { get => ReadUInt32LittleEndian(Data.AsSpan(0x0C)); set => WriteUInt32LittleEndian(Data.AsSpan(0x0C), value); }
    public uint Unkown4 { get => ReadUInt32LittleEndian(Data.AsSpan(0x10)); set => WriteUInt32LittleEndian(Data.AsSpan(0x10), value); }
    public uint Unkown5 { get => ReadUInt32LittleEndian(Data.AsSpan(0x14)); set => WriteUInt32LittleEndian(Data.AsSpan(0x14), value); }


    public ListItem(byte[] Data)
    {
        this.Data = Data;
    }
}
