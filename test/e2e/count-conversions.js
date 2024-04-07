/*
Write a TestCafe javascript file that tests the following:
- The count of forecasts in the forecast table is equal to the number of forecasts in the database
*/
import { Selector } from 'testcafe';

fixture `Count Conversions`
    .page `http://167.86.105.61:5001/`;

test('Count Conversions', async t => {
    // Connects to the API and gets the count of forecasts
    let conversionCount = (await t.request("http://167.86.105.61:5002/currencyconverter")).body.length;

    // Counts the number of table rows in the forecast table.
    let tableRowCount = Selector("table#conversions tbody tr").count;

    // Asserts that the count of forecasts in the forecast table is equal to the number of forecasts in the database from the API.
    await t.expect(tableRowCount).eql(conversionCount);
});