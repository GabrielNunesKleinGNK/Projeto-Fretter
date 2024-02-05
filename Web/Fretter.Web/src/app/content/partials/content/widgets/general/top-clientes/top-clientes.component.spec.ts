import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopClientesComponent } from './top-clientes.component';

describe('AuthorProfitComponent', () => {
  let component: TopClientesComponent;
  let fixture: ComponentFixture<TopClientesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopClientesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopClientesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
