cp Doxyfile ../../master/Doxyfile
mv ../doc-tools ../.doc-tools
cd ../../master
doxygen
cp -a gh-pages/html/* ../gh-pages
cd ../gh-pages
mv .doc-tools doc-tools
