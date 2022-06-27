importScripts('loadash.js');
importScripts('jszip.js');
importScripts('excel-builder.dist.js');

onmessage = function (oEvent) {
    var h;
    try {
        var workbook = ExcelBuilder.Builder.createWorkbook();
        var worksheet, worksheet1, worksheet2, worksheet3, worksheet4, worksheet5;
        var stylesheet = workbook.getStyleSheet();
        var white = 'FFFFFFFF';
        var header = stylesheet.createFormat({
            font: { bold: true, color: white }, fill: { type: 'pattern', patternType: 'solid', fgColor: '3c922d' }
        });
        var obj = oEvent.data;
        if (obj.cnt != null) {
            worksheet = workbook.createWorksheet({ name: obj.nm });
            var widthcolumn = [];
            for (var i = 0; i < obj.cnt; i += 1) {
                widthcolumn.push({ width: 20 });
            }
            worksheet.setColumns(widthcolumn);
            worksheet.sheetView.showGridLines = false;
            worksheet.setData(obj.dt);
            workbook.addWorksheet(worksheet);
        }
        ExcelBuilder.Builder.createFile(workbook).then(function (data) {
            if (navigator.appVersion.toString().indexOf('.NET') > 0) {
                h = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet¯" + data;
            } else {
                h = "data:application/vnd.openxmlformats-officedocument.spreadsheetml.sheet;base64," + data;
            }
            postMessage(h);
        });
    } catch (e) { postMessage({ t: "e", d: e.stack }); }
};