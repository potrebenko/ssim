namespace SSIM.Parser;

public static class ConstantValues
{
    public static ReadOnlySpan<byte> RecordType1 => "1"u8;
    public static ReadOnlySpan<byte> RecordType2 => "2"u8;
    public static ReadOnlySpan<byte> RecordType3 => "3"u8;
    public static ReadOnlySpan<byte> RecordType4 => "4"u8;
    public static ReadOnlySpan<byte> RecordType5 => "5"u8;
    public static ReadOnlySpan<byte> TitleOfContents => "AIRLINE STANDARD SCHEDULE DATA SET"u8;
    public static ReadOnlySpan<byte> RecordSerialNumber => "000001"u8;
}