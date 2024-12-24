using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace SSIM.Parser;

[SuppressMessage("Reliability",
    "CA2018:\'Buffer.BlockCopy\' expects the number of bytes to be copied for the \'count\' argument")]
public class JsonComposer : IDataComposer
{
    public event EventHandler<(byte[] buffer, int position)> OnFlush;

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
        CheckBuffer(1);

        _buffer[_position] = OpenBracket;
        _position++;
    }

    public void AppendCloseBracket()
    {
        CheckBuffer(1);

        _buffer[_position] = CloseBracket;
        _position++;
    }

    public void AppendCloseCurlyBrace()
    {
        CheckBuffer(1);

        _buffer[_position] = CloseCurlyBrace;
        _position++;
    }

    public void AppendOpenCurlyBrace()
    {
        CheckBuffer(1);

        _buffer[_position] = OpenCurlyBrace;
        _position++;
    }

    public void AppendComma()
    {
        CheckBuffer(1);

        _buffer[_position] = Comma;
        _position++;
    }

    public void AppendField(byte[] fieldName, byte[] fieldValue, bool setComma = true)
    {
        CheckBuffer(fieldName.Length + fieldValue.Length + 6); // 6 -> "":"",

        _buffer[_position] = Quote;
        _position++;
        if (fieldName.Length > 8)
        {
            Buffer.BlockCopy(fieldName, 0, _buffer, _position, fieldName.Length);
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
        if (fieldValue.Length > 8)
        {
            Buffer.BlockCopy(fieldValue, 0, _buffer, _position, fieldValue.Length);
            _position += fieldValue.Length;   
        }
        else
        {
            for (int i = 0; i < fieldValue.Length; i++)
            {
                _buffer[_position++] = fieldValue[i];
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

    public void AppendField(byte[] fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true)
    {
        CheckBuffer(fieldName.Length + valueLength + 6); // 6 -> "":"",

        _buffer[_position] = Quote;
        _position++;
        if(fieldName.Length > 8)
        {
            Buffer.BlockCopy(fieldName, 0, _buffer, _position, fieldName.Length);
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

    public void AppendFieldRightJustified(byte[] fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true)
    {
        while (record[offset] == Blank && valueLength > 0)
        {
            offset++;
            valueLength--;
        }

        AppendField(fieldName, record, offset, valueLength, setComma);
    }

    public void AppendFieldLeftJustified(byte[] fieldName, byte[] record, int offset, int valueLength,
        bool setComma = true)
    {
        while (record[offset + valueLength - 1] == Blank && valueLength > 0)
        {
            valueLength--;
        }

        AppendField(fieldName, record, offset, valueLength, setComma);
    }

    public override string ToString()
    {
        return Encoding.UTF8.GetString(_buffer, 0, _position);
    }

    private void CheckBuffer(int length)
    {
        if (_position + length >= _bufferSize)
        {
            Flush();
            _position = 0;
        }
    }

    public void Flush()
    {
        OnFlush?.Invoke(this, (_buffer, _position));
    }
}