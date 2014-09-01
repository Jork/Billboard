module Billboard.ViewModels {

	export module Helpers {

		export function textCssColor(text: string): string {
			var hash: number = textHashCode(text);
			return 'tileColor' + ((hash >> 8) & 0xf).toString();
		}

		export function textHashCode(text: string): number {
			var hash: number = 0;
			var charCode: number;

			if (text.length == 0)
				return hash;

			for (var index: number = 0; index < text.length; index++) {
				charCode = text.charCodeAt(index);
				hash = ((hash << 5) - hash) + charCode;
				hash &= 0xffffffff;
			}

			return hash;
		}

	}

}