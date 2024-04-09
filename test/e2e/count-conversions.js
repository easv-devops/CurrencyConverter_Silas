import { Selector } from 'testcafe';

fixture `Count Conversions`
    .page `http://167.86.105.61:5001/`;

test('Count Conversions', async t => {
    let conversionCount = (await t.request("http://167.86.105.61:5002/currencyconverter")).body.length;
    
    let tableRowCount = Selector("table#history tbody tr").count;
    
    await t.expect(tableRowCount).eql(conversionCount);
});