/// <reference path="../Scripts/typings/jquery.transit/jquery.transit.d.ts" />

module Billboard.View {

	export function widthCollapseRemove(element: HTMLElement, index: number, item: any) {
		var e = $(element);
		e.transition({ width: 0 }, 300, 'ease', ()=> e.remove());
	}

} 