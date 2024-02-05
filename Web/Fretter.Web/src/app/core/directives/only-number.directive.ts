import { Directive, ElementRef, Input, HostListener } from "@angular/core";

@Directive({
    selector: '[OnlyNumber]'
  })
  export class OnlyNumberDirective {

    private specialKeys: Array<string> = ['Backspace', 'Tab', 'End', 'Home', 'ArrowLeft', 'ArrowRight', 'Del', 'Delete'];
  
    constructor(private el: ElementRef) { }
  
    @Input() OnlyNumber: boolean;
  
    @HostListener('keydown', ['$event']) onKeyDown(event) {
      let e = <KeyboardEvent> event;

      if (this.specialKeys.indexOf(event.key) !== -1) {
        return;
      }

      if (this.OnlyNumber) {
        let patt = /^[0-9]*$/;
        let result = patt.test(event.key)
        
        return result;
    }
  }
}