;(function() {
	'use strict';

	angular
		.module('main')
		.controller('SearchCtrl', SearchCtrl);

	SearchCtrl.$inject = ['PersonalInfoLoader'];

	function SearchCtrl(PersonalInfoLoader) {
		var vm = this;

		vm.SearchingOptions = new PersonalInfoLoader.SearchingOptions();

		// Represents datepicker's state.
		vm.dateprickerPopup = {
			opened: false
		};

		// Open datepicker.
		vm.openDatepickerPopup = function() {
			vm.dateprickerPopup.opened = true;
		};
	}
})();