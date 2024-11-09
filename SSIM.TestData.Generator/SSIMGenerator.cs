namespace SSIM.TestData.Generator;

public class SSIMGenerator
{
    private readonly int _recordsToGenerate;
    private readonly ISSIMRecordGenerator _recordGenerator;
    private readonly IRecordWriter _writer;

    public SSIMGenerator(int recordsToGenerate, ISSIMRecordGenerator recordGenerator, IRecordWriter writer)
    {
        _recordsToGenerate = recordsToGenerate;
        _recordGenerator = recordGenerator;
        _writer = writer;
    }
    
    public void Generate()
    {
        var recordNumber = 1;
        _writer.Write(_recordGenerator.GenerateHeaderRecord());
        recordNumber++;
        for (int i = 0; i < 4; i++)
        {
            _writer.Write(_recordGenerator.Blank());
        }
        
        _writer.Write(_recordGenerator.GenerateCarrierRecord());
        recordNumber++;
        for (int i = 0; i < 4; i++)
        {
            _writer.Write(_recordGenerator.Blank());
        }
        
        for (int i = 0; i < _recordsToGenerate; i++)
        {
            _writer.Write(_recordGenerator.GenerateFlightRecord(recordNumber));
            recordNumber++;
        }
        
        _writer.Write(_recordGenerator.GenerateTrailerRecord(recordNumber));
        for (int i = 0; i < 4; i++)
        {
            _writer.Write(_recordGenerator.Blank());
        }

        _writer.CloseAndFlush();
    }
}