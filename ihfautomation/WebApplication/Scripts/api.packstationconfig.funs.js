//
// Name: api.packstationconfig.funs.js
// Type: Java script file 
// Description: different functions required for packstation and trolley/chute configuration
//
//$Revision:   1.1  $
//
// Version   Date        Author    Reason
//  1.0      05/04/12    M Khan    Initial Released
//

$(document).ready(function () {

    function PackstationConfigViewModel() {
        var self = this;

        var c = {
            areaid: "",
            value: "",
            text: ""
        };

        self.packstation = ko.observable();
        self.sortarea = ko.observable();

        self.packstations = ko.observableArray();
        self.sortareas = ko.observableArray();

        self.stackToAssign = ko.observable();
        self.stackToRemove = ko.observable();

        self.availableStacks = ko.observableArray();
        self.assignedStacks = ko.observableArray();

        self.GetAvailablePackstations = function () {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                url: 'DataLoaders/DataLoader.svc/Packstations',
                success: function (response) {
                    self.packstations(response);
                },
                error: function (response) {
                    alert("An error occurred while getting all packstations");
                }
            });
        };


        self.GetSortAreas = function () {
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                url: 'DataLoaders/DataLoader.svc/Areas',
                success: function (response) {
                    self.sortareas(response);
                },
                error: function (response) {
                    alert("An error occurred while getting all Sort Areas");
                }
            });
        };


        self.gp = function () {

            if (self.packstation !== undefined) {

                var p = self.packstation();

                if (p.value !== undefined)
                    c.value = p.value;

                if (p.text !== undefined)
                    c.text = p.text;

            }

            if (self.sortarea() !== undefined) {

                var a = self.sortarea();

                if (a.key !== undefined)
                    c.areaid = a.key;

            } else {
                c.areaid = "";
            }

            return c;
        };


        self.GetSelectedSortArea = ko.dependentObservable({
            read: self.sortarea,
            write: function (sortarea) {
                self.sortarea(sortarea);
                self.loadstacks(self.gp());
            }

        });

        self.packstationSelect = ko.dependentObservable({
            read: self.packstation,
            write: function (packstation) {
                self.packstation(packstation);
                self.loadstacks(self.gp());
            }

        });

        self.loadstacks = function (select) {
            if (select !== undefined) {
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(select),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    url: 'DataLoaders/DataLoader.svc/AvailableStacks',
                    success: function (response) {
                        self.availableStacks(response);
                        sortByChuteId();
                    },
                    error: function (response) {
                        alert("An error occurred while getting available stacks");
                    }
                });

                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(select),
                    contentType: 'application/json; charset=utf-8',
                    dataType: "json",
                    url: 'DataLoaders/DataLoader.svc/AssignedStacks',
                    success: function (response) {
                        self.assignedStacks(response);
                        sortByChuteId();
                    },
                    error: function (response) {
                        alert("An error occurred while getting assigned stacks");
                    }
                });
            }
            else {
                self.availableStacks.removeAll();
                self.assignedStacks.removeAll();
            }

        }




        self.assignStack = function (stack) {
            self.stackToAssign(stack);

            $.ajax({
                type: 'POST',
                data: JSON.stringify(self.stackToAssign()),
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                url: 'DataLoaders/DataLoader.svc/AssignStack',
                error: function (response) {
                    alert("An error occurred while assigning stack");
                }
            });

            self.assignedStacks.push(stack);
            self.availableStacks.remove(stack);

            sortByChuteId();
        };

        self.removeStack = function (stack) {
            self.stackToRemove(stack);

            $.ajax({
                type: 'POST',
                data: JSON.stringify(self.stackToRemove()),
                contentType: 'application/json; charset=utf-8',
                dataType: "json",
                url: 'DataLoaders/DataLoader.svc/RemoveStack',
                error: function (response) {
                    alert("An error occurred while removing stack");
                }
            });

            self.availableStacks.push(stack);
            self.assignedStacks.remove(stack);

            sortByChuteId();
        };

        self.GetAvailablePackstations();
        self.GetSortAreas();

        function sortByChuteId() {
            self.availableStacks.sort(function (a, b) {
                return a.ChuteId < b.ChuteId ? -1 : 1;
            });

            self.assignedStacks.sort(function (a, b) {
                return a.ChuteId < b.ChuteId ? -1 : 1;
            });
        }

    }; //view model


    var viewModel = new PackstationConfigViewModel();
    ko.applyBindings(viewModel);

});                   //ready