;(function() {
	'use strict';

	angular
		.module('main')
		.controller("PersonalInfoCtrl", PersonalInfoCtrl);

	PersonalInfoCtrl.$inject = ['PersonalInfoLoader'];

	function PersonalInfoCtrl(PersonalInfoLoader) {

		var vm = this,
			currentPage = 1,
			pageSize = 10,
			allDataLoaded = false;

		vm.PersonalInfo = [];
		vm.SearchingOptions = new PersonalInfoLoader.SearchingOptions();

		/**
		 * Load personal data and set value of context variables.
		 */
		vm.getInfo = function() {
			PersonalInfoLoader.Load(pageSize, currentPage, vm.SearchingOptions)
				.success(function(data) {
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
	}
})();