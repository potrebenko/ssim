using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace SSIM.Parser;

[SuppressMessage("Reliability",
    "CA2018:\'Buffer.BlockCopy\' expects the number of bytes to be copied for the \'count\' argument")]
public class JsonComposer : IDataComposer
{
    public event Action<byte[], int> OnFlush;

    private const byte OpenCurlyBrace = (byte)'{';
    private const byte CloseCurlyBrace = (byte)'}';
    private const byte OpenBracket = (byte)'[';
    private const byte CloseBracket = (byte)']';
    private const byte Comma = (byte)',';
    private const byte Quote = (byte)'"';
    private const byte Colon = (byte)':';
    private const byte Blank = (byte)' ';

    private int _position = 0;
    private readonly int _bufferSize;
    private readonly byte[] _buffer;

    public JsonComposer(int bufferSize)
    {
        _bufferSize = bufferSize;
        _buffer = new byte[_bufferSize];
    }

    public ReadOnlyCollection<byte> InternalBuffer => Array.AsReadOnly(_buffer);

    public int Position => _position;

    public void AppendOpenBracket()
    {
        _buffer[_position] = OpenBracket;
        _position++;
    }

    public void AppendCloseBracket()
    {
        _buffer[_position] = CloseBracket;
        _position++;
    }

    public void AppendCloseCurlyBrace()
    {
        _buffer[_position] = CloseCurlyBrace;
        _position++;
    }

    public void AppendOpenCurlyBrace()
    {
        _buffer[_position] = OpenCurlyBrace;
        _position++;
    }

    public void AppendComma()
    {
        _buffer[_position] = Comma;
        _position++;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendField(ReadOnlySpan<byte> fieldName, ReadOnlySpan<byte> record, bool setComma = true)
    {
        _buffer[_position] = Quote;
        _position++;
        if (fieldName.Length > 8)
        {
            fieldName.CopyTo(_buffer.AsSpan(_position));
            _position += fieldName.Length;
        }
        else
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                _buffer[_position++] = fieldName[i];
            }    
        }
        _buffer[_position] = Quote;
        _position++;
        _buffer[_position] = Colon;
        _position++;
        _buffer[_position] = Quote;
        _position++;
        if (record.Length > 8)
        {
            record.CopyTo(_buffer.AsSpan(_position));
            _position += record.Length;   
        }
        else
        {
            for (int i = 0; i < record.Length; i++)
            {
                _buffer[_position++] = record[i];
            }    
        }
        _buffer[_position] = Quote;
        _position++;

        if (setComma)
        {
            _buffer[_position] = Comma;
            _position++;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendField(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength, bool setComma = true)
    {
        _buffer[_position] = Quote;
        _position++;
        if(fieldName.Length > 8)
        {
            fieldName.CopyTo(_buffer.AsSpan(_position));
            _position += fieldName.Length;
        }
        else
        {
            for (int i = 0; i < fieldName.Length; i++)
            {
                _buffer[_position++] = fieldName[i];
            }
        }
        _buffer[_position] = Quote;
        _position++;
        _buffer[_position] = Colon;
        _position++;
        _buffer[_position] = Quote;
        _position++;
        if (valueLength > 8)
        {
            Buffer.BlockCopy(record, offset, _buffer, _position, valueLength);
            _position += valueLength;
        }
        else
        {
            for (int i = 0; i < valueLength; i++)
            {
                _buffer[_position++] = record[offset + i];
            }    
        }
        
        _buffer[_position] = Quote;
        _position++;

        if (setComma)
        {
            _buffer[_position] = Comma;
            _position++;
        }
    }

    public void AppendFieldRightJustified(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength, bool setComma = true)
    {
        while (valueLength > 0 && offset < record.Length && record[offset] == Blank)
        {
            offset++;
            valueLength--;
        }

        AppendField(fieldName, record, offset, valueLength, setComma);
    }

    public void AppendFieldLeftJustified(ReadOnlySpan<byte> fieldName, byte[] record, int offset, int valueLength, bool setComma = true)
    {
        while (valueLength > 0 && offset + valueLength <= record.Length && record[offset + valueLength - 1] == Blank)
        {
            valueLength--;
        }

        AppendField(fieldName, record, offset, valueLength, setComma);
    }

    public void CheckBuffer(int size)
    {
        if (_position + size >= _bufferSize)
        {
            Flush();
            _position = 0;
        }
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(_buffer, 0, _position);
    }

    public void Flush()
    {
        OnFlush?.Invoke(_buffer, _position);
    }
}