namespace SSIM.Parser;

public class SSIMParser
{
    private readonly ISSIMReader _fileReader;
    private readonly IFileWriter _fileWriter;
    private readonly IDataComposer _composer;
    private readonly byte[] _buffer;
    private readonly int _maxBufferLength;
    
    public SSIMParser(ISSIMReader fileReader, IFileWriter fileWriter, IDataComposer composer, int bufferSize = 20_100)
    {
        _fileWriter = fileWriter;
        _fileReader = fileReader;
        _composer = composer;

        _buffer = new byte[bufferSize];
        _maxBufferLength = bufferSize;
    }

    public int Parse()
    {
        _composer.AppendOpenBracket();

        var records = _fileReader.ReadHeaders();
        records[0].ParseHeaderRecord(_composer);
        records[1].ParseCarrierRecord(_composer);

        var parsedRecords = 2;
        while (true)
        {
            var offset = 0;
            _fileReader.ReadRecord(_buffer, 0, _maxBufferLength, out var readLength);
            if (readLength == 0)
            {
                break;
            }
            
            while (offset + Constants.RecordLength <= readLength)
            {
                var type = _buffer[offset];
                if (type == ConstantValues.RecordType3[0])
                {
                    _buffer.ParseFlightLegRecord(offset, _composer);
                    parsedRecords++;
                }
                else if (type == ConstantValues.RecordType4[0])
                {
                    _buffer.ParseSegmentDataRecord(offset, _composer);
                    parsedRecords++;
                }
                else if (type == ConstantValues.RecordType5[0])
                {
                    _buffer.ParseTrailerRecord(offset, _composer);
                    parsedRecords++;
                }

                offset += Constants.RecordLength;
            }
        }

        _composer.AppendCloseBracket();
        
        _fileReader.Dispose();
        _composer.Flush();
        _fileWriter.Dispose();
        return parsedRecords;
    }
}