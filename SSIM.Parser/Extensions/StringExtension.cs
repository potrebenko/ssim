using System.Runtime.CompilerServices;
using System.Text;

namespace SSIM.Parser;

public static class StringExtension
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte[] ToBytes(this string value)
    {
        return Encoding.ASCII.GetBytes(value);
    }
}