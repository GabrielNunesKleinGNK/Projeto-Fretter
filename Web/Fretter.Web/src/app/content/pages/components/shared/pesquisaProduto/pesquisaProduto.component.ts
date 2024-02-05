import { Component, OnInit, ViewChild } from '@angular/core';
import { ProdutoService } from '../../../../../core/services/produto.service';
import { Produto } from '../../../../../core/models/Fusion/produto';
import { MatDialogRef, MatPaginator, MatTableDataSource } from '@angular/material';

@Component({
  selector: 'm-pesquisa-produto',
  templateUrl: './pesquisaProduto.component.html'
})
export class PesquisaProdutoComponent implements OnInit {
  @ViewChild(MatPaginator) paginator: MatPaginator;
  
  displayedColumns: string[] = ['sku', 'nome', 'preco', 'altura', 'largura', 'comprimento', 'peso', 'actions'];

  dataSource: MatTableDataSource<Produto> = new MatTableDataSource(Array<Produto>());
  descricao: string;
  viewLoading: boolean = false;

  produtoList: Array<Produto>;

  pageSize: number = 5;
  currentPage: number = 0;
  resultsLength: number = 0;
  
  start: number = 0;
  end: number = 5;

  constructor(
    public dialogRef: MatDialogRef<PesquisaProdutoComponent>,
    private _service: ProdutoService) { }

  ngOnInit() {
    this.dataSource.data = new Array<Produto>();
  }

  pesquisar(){
    this.viewLoading = true;
    this._service.getAllProdutoPorDescricao(this.descricao).subscribe(res => {
      this.produtoList = res;
      this.setProdutoList();
      this.viewLoading = false;
    });
  }

  selecionar(model: any){
    this.dialogRef.close({ data: model });
  }

  pageChange(event){
    this.currentPage = event.pageIndex;
    this.pageSize = event.pageSize;

    this.start = this.currentPage * this.pageSize;
    this.end = (this.currentPage + 1) * this.pageSize;
    
    this.setProdutoList();
	}

  setProdutoList() {
    this.dataSource.data = this.produtoList.slice(this.start, this.end);
    this.resultsLength = this.produtoList.length;
  }
}
