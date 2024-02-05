import { Directive, ElementRef, Input, HostListener } from "@angular/core";

@Directive({
    selector: '[OnlyLetter]'
  })
export class OnlyLetterDirective {

  constructor(private el: ElementRef) { }

  @Input() OnlyLetter: boolean;

  @HostListener('keydown', ['$event']) onKeyDown(event) {
    let e = <KeyboardEvent> event;
    if (this.OnlyLetter) {
      let patt = /[a-zA-Z\u00C0-\u00FF ]+/i;
      let result = patt.test(event.key);
      return result;
    }
  }
}