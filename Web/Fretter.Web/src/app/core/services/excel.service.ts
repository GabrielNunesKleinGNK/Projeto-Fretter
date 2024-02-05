import { Injectable } from '@angular/core';
import * as Excel from "exceljs/dist/exceljs.min.js";
import * as fs from 'file-saver';
import { DatePipe } from '@angular/common';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root'
})
export class ExcelService {

  constructor(private datePipe: DatePipe) {

  }

  generateExcel(fileName : string, dados : any[], modelo : any) {
	let columns = [];
	let rows = [];
	Object.keys(modelo).forEach(col => {
		columns.push( { name : modelo[col].titulo, filterButton: true });
	});
    let workbook = new Excel.Workbook();
	let worksheet = workbook.addWorksheet('Dados');
	worksheet.properties.defaultColWidth = 25;
	dados.forEach(row => {
		let properties = Object.keys(modelo);
		let rowValues = [];
		properties.forEach(property => {
			if (modelo[property] !== undefined) {
				let titulo = modelo[property].titulo;

				if (modelo[property].tipo == "date" && this.validateLineValue(row[property])) {
					rowValues.push(moment(row[property]).format("DD/MM/YYYY"));
				} 
				else if (modelo[property].tipo == "datetime" && this.validateLineValue(row[property])) {
					rowValues.push(moment(row[property]).format("DD/MM/YYYY HH:MM:SS"));
				}
				else if (modelo[property].tipo == "boolean") {
					rowValues.push(modelo[property]);
				} 
				else {
					rowValues.push(row[property]);
				}
			}
		});

		rows.push(rowValues);
	});

	worksheet.addTable({
		name: 'Dados',
		ref: 'A1',
		headerRow: true,
		totalsRow: false,
		style: {
			theme: 'TableStyleMedium1',
			showRowStripes: true,
		},
		columns: columns,
		rows: rows,
	});

	workbook.xlsx.writeBuffer().then((data) => {
      let blob = new Blob([data], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
      fs.saveAs(blob, fileName);
    })

  }

  ajustarDadosExport(dados: any, modelo: any) {
	dados.forEach(item => {
		let properties = Object.keys(item);
		properties.forEach(property => {
			if (modelo[property] !== undefined) {
				let titulo = modelo[property].titulo;

				if (modelo[property].tipo == "date") {
					item[titulo] = moment(item[property]).format("DD/MM/YYYY");
				} else if (modelo[property].tipo == "datetime") {
					item[titulo] = moment(item[property]).format("DD/MM/YYYY HH:MM:SS");
				}
				else if (modelo[property].tipo == "boolean") {
					item[titulo] = modelo[property][item[property]];
				} else {
					item[titulo] = item[property];
				}
			}
			delete item[property];
		});
  	});
  }

  private validateLineValue(lineValue: string): boolean {
	return (lineValue != undefined && lineValue != '' && lineValue != null)
  }
}