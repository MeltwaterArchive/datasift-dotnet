#!/bin/bash
#-v

export BASE_DIR="$( cd "$( dirname $0 )/../../../.." && pwd )/"

source ${BASE_DIR}ms-tools/doc-tools/docathon/sub/make-docs-util-defs.sh
initialise $*

### .NET-specific parameters
parameters "dotnet"

### installation of .NET-specific tools
message 'installing tools'
sudo apt-get install git
sudo apt-get install doxygen

pre_build

### .NET-specific build steps

message "preparing to build documents"
cp ${GH_PAGES_DIR}doc-tools/Doxyfile ${CODE_DIR}Doxyfile ; stop_on_error
mv ${GH_PAGES_DIR}doc-tools ${GH_PAGES_ALT_DIR}.doc-tools ; stop_on_error

### (build/copy docs steps commbined in this case)
(
	message "building documents"
	cd ${CODE_DIR}datasift/doc ; stop_on_error
	doxygen ; stop_on_error
	cp -a ${GH_PAGES_DIR}html/* ${GH_PAGES_DIR} ; stop_on_error
) || error "stopped parent"

message "tidying after document build"
mv ${GH_PAGES_DIR}.doc-tools ${GH_PAGES_ALT_DIR}doc-tools ; stop_on_error

(
	cd ${GH_PAGES_DIR} ; stop_on_error
	git add *
) || error "stopped parent"

post_build

finalise