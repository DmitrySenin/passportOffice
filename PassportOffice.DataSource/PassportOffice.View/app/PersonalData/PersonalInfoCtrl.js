;(function() {
	'use strict';

	angular
		.module('main')
		.controller("PersonalInfoCtrl", PersonalInfoCtrl);

	PersonalInfoCtrl.$inject = ['PersonalInfoLoader', 'EventNames', '$rootScope'];

	function PersonalInfoCtrl(PersonalInfoLoader, EventNames, $rootScope) {

		var vm = this,
			currentPage = 1,
			pageSize = 10,
			allDataLoaded = false,
			fullSort = false,
			searchingOptions = new PersonalInfoLoader.SearchingOptions();

		vm.PersonalInfo = [];

		/**
		 * Load personal data and set value of context variables.
		 */
		vm.getInfo = function() {
			PersonalInfoLoader.Load(pageSize, currentPage, fullSort, searchingOptions)
				.success(function(data) {
					// Handle gathered data.
					data.map(function(item, index, arr) {
						arr[index].BirthdayDate = new Date(item.BirthdayDate);
						arr[index].PassportIssueDate = new Date(item.PassportIssueDate);
						vm.PersonalInfo.push(arr[index]);
					});

					// If loaded portion of data less than requested
					if(data.length < pageSize) {
						allDataLoaded = true;
					}


					vm.InfiniteScroll.inLoading = false;
				})
				.error(function(error) {
					vm.error = error;

					vm.InfiniteScroll.inLoading = false;
				});
		}

		// Load personal data.
		vm.getInfo();

		// Inifinite scroll.
		vm.InfiniteScroll = {
			inLoading: false,
			nextPage: function() {
				if(!allDataLoaded) {
					this.inLoading = true;
					currentPage++;
					vm.getInfo();
				}
			}
		};

		/**
		 * Handles of changing searching parameters.
		 * @param  {Object} event Description of handled event.
		 * @param  {Object} SearchingOptions Object with criteria of data selection.
		 * @param  {Boolean} FullSort Flag which identifies that data should be sorted by all discussed fields.
		 */
		$rootScope.$on(EventNames.Search, function(event, SearchingOptions, FullSort) {
			fullSort = FullSort;
			searchingOptions = SearchingOptions;
			currentPage = 1;
			allDataLoaded = false;
			vm.PersonalInfo = [];
			vm.getInfo();
		});
	}
})();