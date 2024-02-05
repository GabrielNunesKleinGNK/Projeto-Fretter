import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { TopUsuariosComponent } from './top-usuarios.component';

describe('AuthorProfitComponent', () => {
  let component: TopUsuariosComponent;
  let fixture: ComponentFixture<TopUsuariosComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ TopUsuariosComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(TopUsuariosComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
